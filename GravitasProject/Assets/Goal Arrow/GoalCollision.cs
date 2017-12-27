using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour {

    GoalManagerScript goalMan;

    private void OnTriggerEnter(Collider other)
    {
        goalMan.NextGoal();
        Destroy(this.gameObject);
    }


    // Use this for initialization
    void Start () {

        goalMan = GameObject.FindObjectOfType<GoalManagerScript>();
		
	}
	
	
}
