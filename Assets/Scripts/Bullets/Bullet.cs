using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float pierce;
    public float knockback;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        Invoke("destroy", 2f);
    }

    void destroy(){
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.tag == "Enemy"){
            other.GetComponent<EnemyBehavoir>().takeDamage(damage, knockback);
            if(pierce == 1){
                destroy();
            }
            pierce--;
        }
    }


    
}

