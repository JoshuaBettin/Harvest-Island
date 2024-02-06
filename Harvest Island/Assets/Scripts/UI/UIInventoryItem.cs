using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField]
    private Image itemImage, itemImageBackground;
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Color selectedColor;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

    private bool empty = true;

    private void Awake()
    { 
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
        this.empty = true;
    }

    public void Deselect()
    {
        this.itemImageBackground.color = Color.white;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityText.text = quantity.ToString();
        this.empty = false;
    }

    public void Select()
    {
        this.itemImageBackground.color = selectedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.empty == true) return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseButtonClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // just needed for onBeginDrag and onEndDrag to work
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        if (this.empty == true) return;

        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }
}
