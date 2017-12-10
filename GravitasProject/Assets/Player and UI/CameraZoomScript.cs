using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomScript : MonoBehaviour
{

    // A script to handle zooming of a camera child of an object.

    // Sensible limits on the max and min zoom of the camera.
    // These have been made public as they are likely to change with the scale of the environment, and so can be tailored.
    public float ScrollIncrement = 10f;         // The zoom rate of the camera
    public float ScrollUpperLimit = -10f;       // The most zoomed in the camera can be
    public float ScrollLowerLimit = -30f;       // the most zoomed out the camera can be


    // As the Square Root operation is expensive, the upper and lower values are converted to square values for easy comparison to distance.
    float squareUpperLimit;                     
    float squareLowerLimit;

    // Use this for initialization
    void Start()
    {

        // The Square Limits are calculated on Start
        squareUpperLimit = ScrollUpperLimit * ScrollUpperLimit;
        squareLowerLimit = ScrollLowerLimit * ScrollLowerLimit;

    }

    // Update is called once per frame
    void Update()
    {
        // The script will continuously check if the camera is outside the allowable distance.
        // if it is, it will bring it within the limits,
        // if not it will allow the user to move it using the scroll wheel.
        // This script should only move the camera towards or away fromt he object in a direct line, and not in any other direction.


        float distanceToObject = this.transform.localPosition.sqrMagnitude;                     // The square Magnitude is calculated


        if (distanceToObject < squareUpperLimit)                                                // Check if the camera is closer to the object than it should be
        {
            Vector3 Zoom = this.transform.localPosition.normalized * -ScrollUpperLimit;         // A unit vector is created from the current position vector and scaled to be within the limits.
            this.transform.localPosition = Zoom;                                                // The camera position is then set to that value.
        }

        else if (distanceToObject > squareLowerLimit)
        {
            Vector3 Zoom = this.transform.localPosition.normalized * -ScrollLowerLimit;         // A unit vector is created from the current position vector and scaled to be within the limits.
            this.transform.localPosition = Zoom;                                                // The camera position is then set to that value.
        }
        else
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");                                  // If the camera is within the limits, get the mouse inputs
            if (scroll != 0)                                                                    // If the mouse has had an input
            {
                Vector3 Zoom = this.transform.localPosition.normalized * ScrollIncrement;       // create a unit vector for movement per unit mouse scroll
                this.transform.localPosition -= Zoom * scroll;                                  // A positive scroll should correlate to reduced distance between the camera and it's parent, so the value is subtracted.
            }
        }
    }
}