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

    bool MoveUp, MoveDown, MoveLeft, MoveRight;

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


        // Move Vertical

        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * Speed * Time.deltaTime * 100);


        //animation controlling

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) MoveUp = true;
        else MoveUp = false;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) MoveLeft = true;
        else MoveLeft = false;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) MoveRight = true;
        else MoveRight = false;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) MoveDown = true;
        else MoveDown = false;

        anim.SetBool("MoveUp", MoveUp);
        anim.SetBool("MoveLeft", MoveLeft);
        anim.SetBool("MoveRight", MoveRight);
        anim.SetBool("MoveDown", MoveDown);

        // maybe adapt those to make the transition to walk left/right smoother
        if (x <= 0.5 && x >= -0.5) anim.SetBool("Xequal0", true);   
        else anim.SetBool("Xequal0", false);                        
        if (y <= 0.5 && y >= -0.5) anim.SetBool("Yequal0", true);
        else anim.SetBool("Yequal0", false);
    }


}
