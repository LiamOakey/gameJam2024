using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    bool canMove = true;
    public float movementSpeed = 1f;



    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update() {
        if(canMove){

            Transform enemyPosition = gameObject.GetComponent<Transform>();
            Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();

            //Check if playerPosition exists
            if(playerPosition){
                Vector2 direction = (playerPosition.position - transform.position).normalized;
                enemyPosition.Translate(direction* movementSpeed *  Time.deltaTime);
            }

            
        }
    }

}