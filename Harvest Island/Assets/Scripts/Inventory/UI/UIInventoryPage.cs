using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefab;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private UIMouseFollower mouseFollower;

        [SerializeField]
        private UIItemActionPanel actionPanel;

        [SerializeField]
        private UIAgentWeaponPanel weaponPanel;

        private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentlyDraggedIndex = -1;

        public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

        public event Action<int, int> OnSwapItems;

        private bool isHidden;
        public bool  IsHidden { get => isHidden; }

        // Start is called before the first frame update
        void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        internal void ResetAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, contentPanel);
                listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemEndDrag += HandleEndDrag;
            }
        }

        public void UpdateDescription(int itemIndex, Sprite sprite, string title, string description)
        {
            itemDescription.SetDescription(sprite, title, description);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            itemDescription.ResetDescription();
            ResetSelection();

            isHidden = false;
        }

        public void Hide()
        {
        
            gameObject.SetActive(false);
            actionPanel.Toggle(false);
            ResetDraggedItem();

            isHidden = true;
        }

        public void UpdateData(int itemIndex, Sprite sprite, int quantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(sprite, quantity);
            }
        }

        private void HandleItemSelection(UIInventoryItem UIItem)
        {
            int index = listOfUIItems.IndexOf(UIItem);
            if (index == -1) return;

            OnDescriptionRequested?.Invoke(index);
        }

        private void HandleShowItemActions(UIInventoryItem UIItem)
        {
            int index = listOfUIItems.IndexOf(UIItem);
            if (index == -1) return;

            OnItemActionRequested?.Invoke(index);
        }

        private void HandleSwap(UIInventoryItem UIItem)
        {
            int index = listOfUIItems.IndexOf(UIItem);
            if (index == -1)
            {
                ResetDraggedItem();
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedIndex, index);
            HandleItemSelection(UIItem);
        }

        private void HandleBeginDrag(UIInventoryItem UIItem)
        {
            int index = listOfUIItems.IndexOf(UIItem);
            if (index == -1) return;

            currentlyDraggedIndex = index;
            HandleItemSelection(UIItem);
            OnStartDragging?.Invoke(index);

        }

        private void HandleEndDrag(UIInventoryItem UIItem)
        {
            ResetDraggedItem();
        }

        public void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentlyDraggedIndex = -1;
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        public void UpdateEquippedItem(Sprite sprite, int durability)
        {

                weaponPanel.SetData(sprite, durability);
        }

        public void UnEquipItem()
        {
            weaponPanel.SetAgentWeaponEmpty();
        }

        public void ResetSelection()
        {
            DeselectAllItems();
            itemDescription.ResetDescription();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem UIItem in listOfUIItems)
            {
                UIItem.Deselect();
            }
            actionPanel.Toggle(false);
        }

        public void DeactivateActionPanel()
        {
            actionPanel.Toggle(false);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }
    }
}