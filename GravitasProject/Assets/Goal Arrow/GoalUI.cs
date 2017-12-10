using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalUI : MonoBehaviour {

    public float ArrowDistanceFromShip = 10;


    // Create Singleton
    public static GoalUI instance = null;

    // Arrow Prefab
    public GameObject arrowPrefab;

    // Arrow object
    private GameObject arrow;

    // set arrow offset
    public Vector3 offset;

    // The Goal
    public GameObject goal;

    // The playership, for ease of calculation
    PlayerShip player;

    

    private void Awake()
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
    void Start()
    {
        // get the goal for the scene if not set
        if (goal == null)
        {
            goal = GameObject.Find("GoalPrefab");
        }

        offset = new Vector3(-20 ,20, 20);

        // Create the arrow, and set parameters
        arrow = Instantiate(arrowPrefab);
        arrow.transform.SetParent(this.transform);
        arrow.transform.position += offset;
        arrow.transform.rotation = this.transform.rotation ;
        arrow.transform.localScale = new Vector3(5, 5, 5);

        // Get the player to reference later
        player = Object.FindObjectOfType<PlayerShip>();
        
    }

    // Update is called once per frame
    void Update()
    {

        // Set the arrow location to be in the direction of the goal

        // Get the direction vector from the ship to the goal
        Vector3 vecToGoal = goal.transform.position - player.transform.position;
        // Set the arrow position to be ArrowDistanceFromShip in the direction of the goal from the ship
        arrow.transform.position = player.transform.position + vecToGoal.normalized * ArrowDistanceFromShip;


        // Set the arrow rotation to be away from the player

        // create a relative vector from the player to the arrow, but set the z component equal to zero.
        Vector3 temp = new Vector3(arrow.transform.position.x - player.transform.position.x, arrow.transform.position.y - player.transform.position.y, 0);
        // create a rotation quaternion, set to be from the arrow to the player
        Quaternion rot = Quaternion.LookRotation(temp);
        // Set the arrow to be "looking at" the player. Add some angles to ensure the arrows are rotated correctly.
        arrow.transform.rotation = rot * Quaternion.Euler(-90, -90, 0);

    }
    
}
