using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance = null;          // Ensure this class is a singleton

    public Vector3 SpawnPoint = new Vector3(0,0,0);      // This is currently the spawnpoint for all enemies


    public EnemyShip enemyPrefab;                        // The Prefab for the enemy

    public float spawnTime = 10f;                       // spawn time between enemies

    float timeOfNextEnemy;                               // Time the next enemy should be spawned


    void CreateEnemy(Vector3 spawnLoc)
    {
        EnemyShip En = Instantiate(enemyPrefab);
        En.transform.position = spawnLoc;
    }


    void EnemyTimer()
        // This will update the time since last Enemy, and spawn enemies if appropriate.
        // It will also contain code for more complex enemy spawning patterns.
    {
        if (Time.time >= timeOfNextEnemy)           // If an enemy should be spawned
        {
            CreateEnemy(SpawnPoint);                // Create the enemy
            timeOfNextEnemy += spawnTime;           // Reset the timer
        }
    }

    void Awake()
    {
        if (instance == null)               // If the static value is null, set it to be this awakened instance.
        {
            instance = this;
        }
        else if (instance != null)          // if the value has been set to another instance of Playership, destroy this instance and preserve the other one.
        {
            Destroy(gameObject);
        }
    }

   

	// Use this for initialization
	void Start () {
        timeOfNextEnemy = spawnTime;
	}

	
	// Update is called once per frame
	void Update () {

        EnemyTimer();
	}
}
