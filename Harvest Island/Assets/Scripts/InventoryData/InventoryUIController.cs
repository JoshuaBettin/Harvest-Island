using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;
    [SerializeField]
    private int inventorySize = 28;

    public void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }

    // https://www.youtube.com/watch?v=sKlAjbqLdAs&list=PLcRSafycjWFegXSGBBf4fqIKWkHDw_G8D&index=6
}
