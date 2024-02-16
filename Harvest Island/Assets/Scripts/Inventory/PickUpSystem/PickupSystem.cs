using Inventory.Data;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;
    [SerializeField]
    private PhotonView pv;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {

            int oldQuantity = item.Quantity;
            int remainingQuantity;

            if (pv.IsMine) remainingQuantity = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            else if (!PhotonNetwork.IsConnected) remainingQuantity = inventoryData.AddItem(item.InventoryItem, item.Quantity); // only for offline testing purposes
            else remainingQuantity = inventoryData.ReturnLastChangedItemQuantity();

            if (remainingQuantity == 0) item.DestroyItem();
            else
            {
                item.Quantity = remainingQuantity;

                    if (oldQuantity > remainingQuantity) item.PlayPickupSound();
            }
        }

    }
}
