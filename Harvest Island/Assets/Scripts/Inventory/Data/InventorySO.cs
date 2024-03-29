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

        private int lastChangedItemQuantity = 1;

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
            ChangeLastChangedItemQuantity(quantity);
            InformAboutChange();
            return quantity;
        }

        /// <summary>
        /// After Adding an item to someones inventory the int is changed
        /// so that others can get the information how many of the item was picked up
        /// </summary>
        /// <returns></returns>
        private void ChangeLastChangedItemQuantity(int quantity)
        {
            lastChangedItemQuantity = quantity;
        }

        /// <summary>
        /// Returns the int in order to update the amount of an item in the world 
        /// after someone picked up an item
        /// </summary>
        /// <returns></returns>
        public int ReturnLastChangedItemQuantity()
        {
            return lastChangedItemQuantity;
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
            if (inventoryItems.Count > itemIndex)
            {

                if (inventoryItems[itemIndex].isEmpty) return;

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

        public void PlaySound(AudioClip audioClip, float volumeScale)
        {
            AudioSource audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            if (audioSource != null) audioSource.PlayOneShot(audioClip, volumeScale);
        }

        /*
        public void PlayAnimation(Animation animation)
        {
            animation.Play();
        }
        */
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