using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship {


    // The maximum force of the enemy ships
    public float maxForce = 1f;
  
    // The Player ship to move towards
    private PlayerShip Target;

    public float collisionEnergyCostMultiplier = 10f;
    public float onContactEnergyCost = 1f;


    void MoveToPoint(Vector3 target)
        // A function to move towards a point (typically the target PlayerShip, but not necessarily)
    {
        Vector3 DirectionToTarget = target - this.transform.position;   // Get the relative vector from this ship to the target
 
        DirectionToTarget.Normalize();          // Normalise it
        DirectionToTarget *= maxForce;

        Move(DirectionToTarget.z , DirectionToTarget.x , 0);          // Move the ship in that direction
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerShip>() != null )
        {
            Target.ApplyDamage(System.Convert.ToInt32(collision.impulse.magnitude * collisionEnergyCostMultiplier + onContactEnergyCost));
        }
        else if (collision.gameObject.GetComponent<GravitySource>() != null)
        {
            Destroy(this.gameObject);
        }
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

