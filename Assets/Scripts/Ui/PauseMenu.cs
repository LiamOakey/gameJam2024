using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseUI;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameIsPaused){
                Continue();
            }else{
                Pause();
            }
        }
    }


    public void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale=0f;
        gameIsPaused = true;
    }

    public void Continue(){
        pauseUI.SetActive(false);
        Time.timeScale=1f;
        gameIsPaused = false;
    }
}
