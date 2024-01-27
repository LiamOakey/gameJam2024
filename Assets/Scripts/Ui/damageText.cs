using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damageText : MonoBehaviour
{

    float moveSpeed = 2f;
    private Color textColor;
    private TextMeshPro textMesh;
    private float disappearTimer = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }

    void Update(){
        transform.position += new Vector3(0,moveSpeed) * Time.deltaTime;
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0){
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0){
                Destroy(gameObject);
            }
        }
    }
    
}
