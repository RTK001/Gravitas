using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    // Properties: Things a ship has

    protected Rigidbody rigid;

    

    public void Move(int forward, int strafeRight, int rotRight)
    {
        // Function called to move the ship
        
        // Convert to floats to 
        float fwd = System.Convert.ToSingle(forward);
        float rgt = System.Convert.ToSingle(strafeRight);
        // float rotrgt = 

        rigid.AddForce((transform.up * fwd + transform.right * rgt));
        rigid.AddTorque(transform.forward * (-rotRight));

        
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

    }
	
	// Update is called once per frame
	void Update () {


    }
}
