using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpin : MonoBehaviour {
    // A Class to implement simple Planet Movement

    public Vector3 startLoc;
    public float angularSpeed = 10f;       // Approximate speed of the Planet
    public Vector3 centreOfRotation = new Vector3(0, 1, 0);     // Centre of rotation - defaults to world origin


    public Vector3 GetPositionAtTime(float timeInFuture)
    // Function to predict where the planet will be at any given time
    {
        Vector3 position = this.transform.position;
        
        return Quaternion.Euler(0, timeInFuture * angularSpeed, 0) * position;
    }


    void Start()
    {
        startLoc = this.transform.position;




    }

    // Update is called once per frame
    void Update () {

        // On each call of Update, rotate the planet around the CentreOfRotation, about the global Y axis, by an angle.
        // The angle is the time since the last frame * the angular speed
        this.transform.RotateAround(centreOfRotation, this.transform.up, Time.deltaTime * angularSpeed);

	}
}
