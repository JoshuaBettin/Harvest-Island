using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float Speed;
    [SerializeField]
    private Rigidbody2D rb;

    SpriteRenderer spriteRenderer;

    PhotonView view; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
        }
    }

    /// <summary>
    /// Führt die Bewegung des Spielers in die gewünschte Richtung aus
    /// und dreht das Prefab je nach Laufrichtung um. 
    /// </summary>
    private void Move()
    {
        // Move Horizontal

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Speed * Time.deltaTime * 100, rb.velocity.y);

        if (Input.GetAxis("Horizontal") < 0 && spriteRenderer.flipX == false)
        {
            spriteRenderer.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0 && spriteRenderer.flipX == true)
        {
            spriteRenderer.flipX = false;
        }

        // Move Vertical

        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * Speed * Time.deltaTime * 100);
    }


}
