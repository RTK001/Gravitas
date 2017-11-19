using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    // Properties: Things a ship has

    protected Rigidbody rigid;
    public Vector3 shipForward;
    public Vector3 shipRight;
    public Vector3 shipUp;

    // Engine Limits
    public float maxSpeed = 1f;
    float sqrMaxSpeed;

    
    public void Move(float forward, float strafeRight, float rotRight)
    {
        // Function called to move the ship using forces
        // References the ship axes

        rigid.AddForce((shipForward * forward + shipRight * strafeRight));
        rigid.AddTorque(shipUp * (-rotRight));

    }

    protected void LimitSpeed()
        // A function to limit the speed of the ship.
        // Typically used for enemies, but may be a useful feature for the player later.
    {
        if (rigid.velocity.sqrMagnitude > sqrMaxSpeed)  // If the ship's speed is greater than the limit (computed in squares for speed)
        {

            rigid.velocity = rigid.velocity.normalized * sqrMaxSpeed;       // set the ship velocity to the speed limit in the current direction.
        }
    }



    // Use this for initialization
    void Start () {

        // Check if the Ship has all the requisite components
        // Output a Debug.Log if not

        // Get the rigid body
        rigid = this.GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.Log("Ship is  missing a Rigid Body Component", this);         // If not, alert the debugger that a rigid body must be assigned.
        }

        // Set the axes of the ship
        // Currently, all ships are created at 90 Degrees around X to the origin.
        shipForward = this.transform.up;
        shipRight = this.transform.right;
        shipUp = this.transform.forward;

        // Set the speed Limit square
        sqrMaxSpeed = maxSpeed * maxSpeed;

    }
	
	// Update is called once per frame
	void Update () {

    }
}
