using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    public Sprite sprite, sprite2;
    public int quantity;
    public string title, description;

    private int currentlyDraggedIndex = -1;

    // Start is called before the first frame update
    void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
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

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUIItems[0].SetData(sprite, quantity);
        listOfUIItems[1].SetData(sprite2, quantity);
        listOfUIItems[0].Deselect();
    }

    public void Hide()
    {
    
        gameObject.SetActive(false);
        

    }


    private void HandleItemSelection(UIInventoryItem UIItem)
    {
        itemDescription.SetDescription(sprite, title, description);
        listOfUIItems[0].Select();
    }

    private void HandleShowItemActions(UIInventoryItem UIItem)
    {
        throw new NotImplementedException();
    }

    private void HandleSwap(UIInventoryItem UIItem)
    {
        int index = listOfUIItems.IndexOf(UIItem);
        if (index == -1)
        {
            mouseFollower.Toggle(false);
            currentlyDraggedIndex = -1;
            return;
        }

        listOfUIItems[currentlyDraggedIndex].SetData(index == 0 ? sprite : sprite2, quantity);
        listOfUIItems[index].SetData(currentlyDraggedIndex == 0 ? sprite : sprite2, quantity);

        mouseFollower.Toggle(false);
        currentlyDraggedIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem UIItem)
    {
        int index = listOfUIItems.IndexOf(UIItem);
        if (index == -1) return;

        currentlyDraggedIndex = index;

        mouseFollower.Toggle(true);
        mouseFollower.SetData(index == 0 ? sprite : sprite2, quantity);
    }

    private void HandleEndDrag(UIInventoryItem UIItem)
    {
        mouseFollower.Toggle(false);
    }

}
