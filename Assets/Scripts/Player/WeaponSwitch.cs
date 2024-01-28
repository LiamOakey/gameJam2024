using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponSwitch : MonoBehaviour
{
    
    private static float switchTime = 30f; //Time between weaponswaps
    private  static float timer = switchTime;

    public TextMeshProUGUI timerText;

    
    int nextWeapon = 0; //Gets incremented for each weapon switched, each weapon will correspond to a number


    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.Round(timer).ToString();

        if(timer < 0){

            if(nextWeapon==0){
                GameObject.FindWithTag("Player").GetComponent<playerShooting>().switchToBanana();
            } else if(nextWeapon == 1){
                GameObject.FindWithTag("Player").GetComponent<playerShooting>().switchToWaterGun();
            } else if(nextWeapon == 2){
                GameObject.FindWithTag("Player").GetComponent<playerShooting>().switchToChicken();
            } else if(nextWeapon == 3){
                GameObject.FindWithTag("Player").GetComponent<playerShooting>().switchToCatLauncher();
            }else if(nextWeapon == 4){
                EnemySpawner.hellMode = true;
            }

            nextWeapon++;
            timer = switchTime;
            
        }

    }


}
