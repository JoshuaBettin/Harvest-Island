using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Inventory.Data
{
    [CreateAssetMenu(menuName = "InventorySO")]
    public class InventorySO : ScriptableObject
    {
        [SerializeField]
        private List<InventoryItem> inventoryItems;
        [SerializeField]
        private int size = 14;
        public int Size { get => size; }

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated; 

        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (!item.IsStackable)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem
            {
                item = item,
                quantity = quantity,
                itemState = new List<ItemParameter>(itemState == null ? item.DefaultParameterList : itemState)
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if(inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity; 
                }
            }
            return 0; 
        }

        private bool IsInventoryFull()
        => inventoryItems.Where(item => item.isEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty) continue; 

                if (inventoryItems[i].item.ID == item.ID)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }

            while(quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }

        internal void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty) continue;
                returnValue[i] = inventoryItems[i];
            }       
            return returnValue;
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = inventoryItems[itemIndex1];
            inventoryItems[itemIndex1] = inventoryItems[itemIndex2];
            inventoryItems[itemIndex2] = item1;
            InformAboutChange(); 
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }

        public void RemoveItem(int itemIndex, int quantity)
        {
            if(inventoryItems.Count > itemIndex)
            {
                
                if (inventoryItems[itemIndex].isEmpty) return;

                // remove item when durability 0 if item has parameter durability
                IDestroyableItem destroyableItem = inventoryItems[itemIndex].item as IDestroyableItem;
                if(destroyableItem != null)
                {
                    UnityEngine.Debug.Log("RemoveItemIfIDestroyable");
                    for (int i = 0; i < inventoryItems[itemIndex].itemState.Count; i++)
                    {
                        if (inventoryItems[itemIndex].itemState[i].value <= 0)
                        {
                            UnityEngine.Debug.Log("RemoveItemIfItemStateValue=0");
                            inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                        }
                    }
                }         //  ? weil selbstgeschrieben eh nö

                int remainingQuantity = inventoryItems[itemIndex].quantity - quantity;
                if (remainingQuantity <= 0)
                {
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                }
                else
                {
                    inventoryItems[itemIndex] = inventoryItems[itemIndex].ChangeQuantity(remainingQuantity);
                }
                InformAboutChange();
            }
        }
    }

    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool isEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
            quantity = 0,
            itemState = new List<ItemParameter>()
        };
    }
}