using Inventory.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private ItemSO InventoryItem;
    [SerializeField]
    private int Quantity;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float duration = 0.3f;
    #endregion

    #region Properties
    public ItemSO InventoryItem1 { get => InventoryItem; set => InventoryItem = value; }
    public int Quantity1 { get => Quantity; set => Quantity = value; }
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    public float Duration { get => duration; set => duration = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
