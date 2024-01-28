using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStart : MonoBehaviour
{
    
    void Update(){
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "End"){
            SceneManager.LoadScene ("Start");
        }
    }
}
