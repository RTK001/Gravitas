using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpin : MonoBehaviour {
    // A Class to implement simple Planet Movement

    public float angularSpeed = 10f;       // Approximate speed of the Planet
    public Vector3 CentreOfRotation = new Vector3(0, 0, 0);     // Centre of rotation - defaults to world origin

	
	// Update is called once per frame
	void Update () {


        // On each call of Update, rotate the planet around the CentreOfRotation, about the global Y axis, by an angle.
        // The angle is the time since the last frame * the angular speed
        this.transform.RotateAround(CentreOfRotation, this.transform.up, Time.unscaledDeltaTime * angularSpeed);
		
	}
}
