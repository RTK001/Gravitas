using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {


    // The maximum force of the enemy ships
    public float maxForce = 1f;
  
    // The Player ship to move towards
    private PlayerShip Target;


    void MoveToPoint(Vector3 target)
        // A function to move towards a point (typically the target PlayerShip, but not necessarily)
    {
        Vector3 DirectionToTarget = target - this.transform.position;   // Get the relative vector from this ship to the target
 
        DirectionToTarget.Normalize();          // Normalise it

        Move(DirectionToTarget.z, DirectionToTarget.x, 0);          // Move the ship in that direction
    }   


    

	
	void Awake () {

        Target = Object.FindObjectOfType<PlayerShip>();

    }
	
	// Update is called once per frame
	void Update () {

        MoveToPoint(Target.transform.position); // Move towards the target
        LimitSpeed();                           // Limit the speed
        
		
	}
}
