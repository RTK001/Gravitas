using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManagerScript : MonoBehaviour {

    // Create goal
    // have array of goals
    // on trigger, destroy one goal, and createa  new one
    // be able to load other scripts, etc as required.

    public static GoalManagerScript instance;

    public Vector3 [] goals;
    public GameObject goalPrefab;
    IEnumerator<Vector3> currentGoalEnumerator;

    GoalUI goalUI;


    // Events system for Game won
    public delegate void GameWinAction();   // A delegate defining the function signature to be called on game win
    public static event GameWinAction OnGameWin;        // The event to be called when the game is won


    void CreateGoal(Vector3 pos)
    {
        GameObject currentGoal = Instantiate(goalPrefab);
        currentGoal.transform.position = pos;

    }

    public void NextGoal()
    {
        // Called to move the goal to the next specified one along the list

        if (currentGoalEnumerator.MoveNext())       // Increment the iterator to the next goal in the array, and excecute the code if there is
        {
            CreateGoal(currentGoalEnumerator.Current);      // Create the new goal
            goalUI.UpdateGoals();                           // Alert the UI that a new goal has been created.
        }
        
        else                            // If there are no new goals to move to:
        {
            if (OnGameWin != null)  // If there are subsctibers to the OnGameWin event
            {
                OnGameWin();
            }
        }   
    }

	// Use this for initialization
	void Start () {

        if (instance == null)               // If the static value is null, set it to be this awakened instance.
        {
            instance = this;
        }
        else if (instance != null)          // if the value has been set to another instance of Playership, destroy this instance and preserve the other one.
        {
            Destroy(gameObject);
        }

        goalUI = GameObject.FindObjectOfType<GoalUI>();

        currentGoalEnumerator = ((IEnumerable<Vector3>) goals).GetEnumerator();

        NextGoal();

    }
	


	// Update is called once per frame
	void Update () {
		
	}
}
