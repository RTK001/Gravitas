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

    void CreateGoal(Vector3 pos)
    {
        GameObject currentGoal = Instantiate(goalPrefab);
        currentGoal.transform.position = pos;

    }

    public void NextGoal()
    {
        // Called to move the goal to the next specified one along the list

        if (currentGoalEnumerator.MoveNext())       // If there is another goal to move to
        {
            CreateGoal(currentGoalEnumerator.Current);
            goalUI.UpdateGoals();
        }
        else
        {
            Debug.Log("End!");
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
