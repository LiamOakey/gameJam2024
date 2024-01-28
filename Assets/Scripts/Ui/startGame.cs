using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    
    void Update(){
        if(Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Start"){
            SceneManager.LoadScene ("Main");
        }
    }
}
