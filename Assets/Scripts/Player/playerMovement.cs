using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed the player moves at
    private Transform transform;
    private Rigidbody2D rb;

    //Links components to variables
    void Awake()
    {
        transform = this.gameObject.GetComponent<Transform>();   
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        movementHandler();
    }

    void movementHandler(){
        if(rb.velocity.x < 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if(rb.velocity.x > 0){
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(movementX, movementY);
        movement = movement.normalized * moveSpeed;
        rb.velocity = movement;
    }


}
