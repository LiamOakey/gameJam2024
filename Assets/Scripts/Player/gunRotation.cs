using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunRotation : MonoBehaviour
{
    Camera cam;
    Vector2 mousePos;
    Rigidbody2D rb;
    public static float angle;

    //Negative describes if the firepoint has been flipped on across the x axis, ie y=-y.
    bool negative = false;


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }
    // Update is called once per frame
    void Update(){
        Debug.Log(negative);
        rotationHandler();
    }


    void rotationHandler(){
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = mousePos - rb.position;
        angle = Mathf.Atan2(lookDirection.y, lookDirection.x) *Mathf.Rad2Deg;

        Transform firePoint = transform.GetChild(0).transform.GetChild(0).transform;

        if(angle > 90 || angle < -90){
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = true;
            if(!negative){
                firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y *-1);
                negative=true;
            }
        } else {
            transform.GetChild(0).GetComponent<SpriteRenderer>().flipY = false;
            if(negative){
                firePoint.transform.localPosition = new Vector3(firePoint.transform.localPosition.x, firePoint.transform.localPosition.y *-1);
                negative=false;
            }
        }
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }

}