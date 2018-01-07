using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{

    GoalManagerScript goalMan;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerShip>())     // Check if the collider is player
        {
            goalMan.NextGoal();         // Advance the level to the next goal
            Destroy(this.gameObject);   // Destroy this goal
        }

    }


    // Use this for initialization
    void Awake()
    {

        goalMan = GameObject.FindObjectOfType<GoalManagerScript>();     // Get the GoalManager
    }
}
