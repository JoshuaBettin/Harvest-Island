using Inventory.UI;
using Inventory.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;
using UnityEngine.InputSystem;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        private AgentWeapon agentWeapon;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        public void Start()
        {
            agentWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<AgentWeapon>(); 

            PrepareUI();
            PrepareInventoryData();

            inventoryUI.Show(); inventoryUI.Hide(); // to fix Bug: else u need to double press "I"-Key at the start
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Debug.Log("I");
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    Debug.Log("Show");
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.Sprite, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                if (inventoryUI.IsHidden)
                {
                    Debug.Log("RightClick");
                    agentWeapon.UseWeapon();
                    if (agentWeapon.Weapon != null) inventoryData.PlaySound(agentWeapon.Weapon.actionSFX, 1f);

                    if (agentWeapon.Weapon != null) inventoryUI.UpdateEquippedItem(agentWeapon.Weapon.Sprite, agentWeapon.Durability);
                    else inventoryUI.UnEquipItem();
                }
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.isEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.Sprite, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);

            inventoryUI.UpdateDescription(itemIndex, item.Sprite, item.Title, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append(  $"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                            $"{inventoryItem.itemState[i].value} / " +
                            $"{inventoryItem.item.DefaultParameterList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty) return;

            inventoryUI.CreateDraggedItem(inventoryItem.item.Sprite, inventoryItem.quantity);
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                return;
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if(itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            inventoryData.PlaySound(destroyableItem.dropSFX, 1f );

            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                return;
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                Debug.Log("Remove Item");
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryData.PlaySound(itemAction.actionSFX, 1f);
                itemAction.PerformAction(GameObject.FindGameObjectWithTag("Player"), inventoryItem.itemState);

                IEquippableItem equippableItem = inventoryItem.item as IEquippableItem;
                if (equippableItem != null)
                {
                    Debug.Log(agentWeapon.Weapon.Sprite + " " + agentWeapon.Durability);
                    inventoryUI.DeactivateActionPanel();

                   // inventoryUI.UpdateEquippedItem(agentWeapon.Weapon.Sprite, agentWeapon.Durability);

                   // agentWeapon.UseWeapon();

                    if (agentWeapon.Weapon != null) inventoryUI.UpdateEquippedItem(agentWeapon.Weapon.Sprite, agentWeapon.Durability);
                   // else inventoryUI.UnEquipItem();
                }
                if (inventoryData.GetItemAt(itemIndex).isEmpty) inventoryUI.ResetSelection();
            }
        }
    }
}