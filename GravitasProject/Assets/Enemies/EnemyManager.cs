using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance = null;          // Ensure this class is a singleton

    public Vector3 SpawnPoint = new Vector3(0,0,0);      // This is currently the spawnpoint for all enemies


    public EnemyShip enemyPrefab;                        // The Prefab for the enemy

    public float spawnTime = 1f;                       // spawn time between enemies

    public int totalEnemies = 10;                       // the total enemies to spawm

    
    void CreateEnemy(Vector3 spawnLoc)
    {
        EnemyShip En = Instantiate(enemyPrefab);
        En.transform.position = spawnLoc;
    }


    IEnumerator EnemyCoRoutine()
    {
        for (int i = 1; i < totalEnemies; i++)
        {
            
            CreateEnemy(SpawnPoint);
            yield return new WaitForSeconds(spawnTime);
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
        StartCoroutine("EnemyCoRoutine");
	}

}
