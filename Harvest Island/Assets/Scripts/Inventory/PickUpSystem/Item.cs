using Inventory.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    #region Variables
    [SerializeField]
    private ItemSO inventoryItem;
    [SerializeField]
    private int quantity;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float duration = 0.3f;
    #endregion

    #region Properties
    public ItemSO InventoryItem { get => inventoryItem; set => inventoryItem = value; }
    public int Quantity { get => quantity; set => quantity = value; }
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    public float Duration { get => duration; set => duration = value; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.Sprite;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimatePickup());
    }

    public void PlayPickupSound()
    {
        audioSource.Play();
    }

    private IEnumerator AnimatePickup()
    {
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        float timer = 0; 
        while(timer < duration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, timer / duration);
            yield return null; 
        }
        transform.localScale = endScale;
        Destroy(this.gameObject); 
    }
}
