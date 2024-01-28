using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    public float movementSpeed = 1f;



    private void Update() {
        if(canMove){

            Transform enemyPosition = gameObject.GetComponent<Transform>();
            Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();

            //Check if playerPosition exists
            if(playerPosition){
                Vector2 direction = (playerPosition.position - transform.position).normalized;
                enemyPosition.Translate((direction* movementSpeed).normalized *  Time.deltaTime);

                if(direction.x < 0){
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                } else if(direction.x > 0){
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            
        }
    }

}