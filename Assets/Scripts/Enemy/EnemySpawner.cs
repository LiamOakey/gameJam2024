using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPoint; //Parent that holds list of children that are potential spawns
    public GameObject enemy1;
    
    public static float spawnDelay = 2f; //Time between spawns
    public static float spawnTimer = spawnDelay; //Timer that counts down

    void Update(){
        continuiousSpawn(enemy1);
    }

    void continuiousSpawn(GameObject enemy){
        spawnTimer-=Time.deltaTime;
        if(spawnTimer < 0){
            spawnEnemy(enemy);
            spawnTimer = spawnDelay;
        }
    }
    //EnemySpawner will spawn at a random spawnpoint under the variable spawnPoint
    void spawnEnemy(GameObject enemy){
        int spawnEnemyNum = (int) Random.Range(0,spawnPoint.transform.childCount-1);
        Instantiate(enemy, spawnPoint.transform.GetChild(spawnEnemyNum).transform.position, Quaternion.identity);
    }
}
