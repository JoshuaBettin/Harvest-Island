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
    [SerializeField]
    private Animator anim; 

    PhotonView view;

    public float x;
    public float y;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
        }

        if (!PhotonNetwork.IsConnected)
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

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") < 0 && this.transform.localScale.x >= 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;

            this.transform.localScale = newScale;
        }
        if (Input.GetAxis("Horizontal") > 0 && this.transform.localScale.x < 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;

            this.transform.localScale = newScale;
        }

        // Move Vertical

        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * Speed * Time.deltaTime * 100);
    }


}
