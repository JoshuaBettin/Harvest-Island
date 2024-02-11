using Inventory.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if(item != null)
        {
            int oldQuantity = item.Quantity;
            int remainingQuantity = inventoryData.AddItem(item.InventoryItem, item.Quantity);

            if (remainingQuantity == 0) item.DestroyItem();
            else
            {
                item.Quantity = remainingQuantity;
                if (oldQuantity > remainingQuantity) item.PlayPickupSound();
            }
        }
    }
}
