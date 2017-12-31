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
    int enumeratorCounter = 0; // Required to count the current goal as Enumerators do not retain the index.

    GoalUI goalUI;


    // Events system for Game won
    public delegate void GameWinAction(int currentGoal, int totalGoals);   // A delegate defining the function signature to be called on game win
    public static event GameWinAction OnGoalComplete;        // The event to be called when a goal is completed


    void CreateGoal(Vector3 pos)
    {
        GameObject currentGoal = Instantiate(goalPrefab);
        currentGoal.transform.position = pos;
        goalUI.UpdateGoals(currentGoal);                           // Alert the UI that a new goal has been created.
    }

    public void NextGoal()
    {


        // Called to move the goal to the next specified one along the list
        if (currentGoalEnumerator.MoveNext())       // Increment the iterator to the next goal in the array, and excecute the code if there is
        {
            CreateGoal(currentGoalEnumerator.Current);      // Create the new goal
        }

        enumeratorCounter++;

        if (OnGoalComplete != null)  // If there are subsctibers to the OnGoalComplete event
        {
            OnGoalComplete(enumeratorCounter, goals.Length);
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
        enumeratorCounter = 0;      // Reset the Enumerator counter to 0, setting it to be one lower than the currentGoalEnumerator (IE/ completed goals, rather than loaded goals)

    }
	

}
