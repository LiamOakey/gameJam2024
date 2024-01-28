using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;
    bool invincible = false;

    BoxCollider2D collider;
    public Slider healthSlider;

    private void Awake() {
        collider = GetComponent<BoxCollider2D>();
    }

    void takeDamage(float damage){

        Debug.Log(damage);

        invincible = true;
        Invoke("resetInvincible", 0.2f);
        health -= damage;
        healthSlider.value = health;


        //death check
        if(health < 1){
            die();
        }
    }

    void OnCollisionStay2D(Collision2D other){
        if(!invincible){
            if(other.gameObject.tag == "Enemy"){
                float damage = other.gameObject.GetComponent<EnemyBehavoir>().damage;
                takeDamage(damage);
            }
        }
    }
    
    void resetInvincible(){
        invincible = false;
    }

    void die(){
        WeaponSwitch.resetTimer();
        EnemySpawner.reset();
        SceneManager.LoadScene("End");
    }
}