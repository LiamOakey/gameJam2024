using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotation : MonoBehaviour
{
    Camera cam;
    Vector2 mousePos;
    Rigidbody2D rb;
    public static float angle;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - rb.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) *Mathf.Rad2Deg;
        if(angle > 90 || angle < -90){
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
        } else {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
        
    }

}