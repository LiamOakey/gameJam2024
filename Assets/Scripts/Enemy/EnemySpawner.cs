using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPoint; //Parent that holds list of children that are potential spawns
    public GameObject enemy1;
    
    public static float spawnDelay = 2f; //Time between spawns
    public static float spawnTimer = spawnDelay; //Timer that counts down

    void Awake(){
        spawnEnemy(enemy1);
        spawnEnemy(enemy1);
    }

    void Update(){
        continuiousSpawn(enemy1);
    }

    void continuiousSpawn(GameObject enemy){
        spawnTimer-=Time.deltaTime;
        if(spawnTimer < 0){
            bool spawned = spawnEnemy(enemy);

            if(spawned == true) spawnTimer = spawnDelay;
        }
    }

    //EnemySpawner will spawn at a random spawnpoint under the variable spawnPoint, it will return true if the enemy is spawned in
    bool spawnEnemy(GameObject enemy){
        int spawnEnemyNum = (int) Random.Range(0,spawnPoint.transform.childCount-1);

        //Ensure enemies are inside the map
        float x = spawnPoint.transform.GetChild(spawnEnemyNum).transform.position.x;
        float y = spawnPoint.transform.GetChild(spawnEnemyNum).transform.position.y;
        if(x < -25 || x > 25 || y > 19 || y < -20) return false;


        Instantiate(enemy, spawnPoint.transform.GetChild(spawnEnemyNum).transform.position, Quaternion.identity);
        return true;
    }
}
