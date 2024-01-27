using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1;
    
    void Start(){
        spawnEnemy(enemy1);
    }

    void spawnEnemy(GameObject enemy){
        Instantiate(enemy, new Vector3(0,0,0), Quaternion.identity);
    }
}
