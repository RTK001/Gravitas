using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour {

    public GameObject GameManager;
    
    

    void OnLevelGoal (int currentGoal, int totalGoals)
    {
        switch (currentGoal)
        {
            case 0:
                break;

            case 1:
                break;

        }

    }

	// Use this for initialization
	void Start () {

        GoalManagerScript.OnGoalComplete += OnLevelGoal;

    }
	
	
}
