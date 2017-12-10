using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrajectoryPoint
{
    // This class defines the container for a trajectory point, and manages the calculations to calculate the trajectory.
    // It uses a forward Euler method to calculate the trajectory - 
            // ie/ it projects the expected movement of the ship forward from the previous trajectory point.

        // The position, velocity and acceletration are calculated and stored in the class as Vector3's
    Vector3 Position;
    Vector3 Velocity;
    Vector3 Acceleration;

    // The getter methods for these vectors - used for the next point to calculate, and to plot the trajectory.
    public Vector3 GetPosition() { return Position; }
    public Vector3 GetVelocity() { return Velocity; }
    public Vector3 GetAcceleration() { return Acceleration; }

    GravityManager gravMan;     // The Gravity Manager is acquired from the the previous point on construction, 
                                // allowing the point to get the acceleration due to gravity at any given point and time.

    public GravityManager GetGravityManager() { return gravMan; }       // The getter to allow the next point to get the gravity manager form this one.


    const int numberOfIterations = 4;           // The number of iterations to produce a stable and accurate result - fine tuned through playtesting.
    float timeIncrement;                        // the time that this point projects the ship forward ahead of the previous point
    const float defaultTimeIncrement = 0.15f;   // the default timeIncrement.

    public float GetTimeIncrement() { return timeIncrement; }       // The getter for the time increment. Used by the TrajectoryPointList Coroutine as a base for the waiting time..


    float projectedTime;                    // The time since the game start that this point is calculating. May later be used for sorting outside the class.

    public float GetProjectedTime() { return projectedTime; }   // The getter for the projectedTime. 

    TrajectoryPoint prevPoint;              // The previosu point in the list - used to project trajectory forward and to get parameters on construction.




    public TrajectoryPoint(Vector3 pos, Vector3 vel, Vector3 acc, GravityManager gravityManager)
    {   // The zero-point (ie/ first in the list) constructor 
        // This will accept all the key calculation objects, that can then be passed down the points in the list.

        Position = pos;     // Set the position, velocity and acceleration to match the Playership
        Velocity = vel;
        Acceleration = acc;

        gravMan = gravityManager;     // Accept the Gravity Manager
        prevPoint = null;              // Set the previous Point to be Null, as this is the first point.

        projectedTime = Time.time;     // sets the projection time as the current time, allowing later points to base their projection time off this.
        timeIncrement = 0;              // As this is the zero point, the time increment is zero.

    }


    public TrajectoryPoint(TrajectoryPoint previousPoint)
    {   // The constructor for following points.
        // Thses can get all relevant information from the previosu point.

        
        prevPoint = previousPoint;      // set the previousPoint

        // Set some initial values for the Position, Velocity and Acceleration.
        Position = previousPoint.GetPosition();
        Velocity = previousPoint.GetVelocity();
        Acceleration = previousPoint.GetAcceleration();


        gravMan = previousPoint.GetGravityManager();    // Get the Gravity Manager from the Previous Point

        timeIncrement = defaultTimeIncrement;           // Uset he default Time increment.
        projectedTime = previousPoint.projectedTime + defaultTimeIncrement;     // Set the projected time to be the time increment ahead of the previous point

        CalculatePoint();           // Calculate the point.
    }


    public void SetEqual(Vector3 pos, Vector3 vel, Vector3 acc)
    {   // Used to re-initialise the points in the list to be at the playerShip location.
        // This is done periodically to compensate for calculation errors, collision and player input.
        Position = pos;
        Velocity = vel;
        Acceleration = acc;
        projectedTime = Time.time;
    }

    

    public void CalculatePoint()
    {   // This method calculates the position, velocity and acceleration of the trajectory point.
        if (prevPoint != null)  // Do not do this calculation on the zero point, as the calculation requires a previous point to project forward.
        {
            projectedTime = prevPoint.projectedTime + timeIncrement;        // Get the most up-to-date world time to update to

            for (int j = 0; j < numberOfIterations; j++)            // THis is an iterative calculation that successively improves accuracy.
            {
                // Set the New velocity to be the previous velocity + the mean(average) acceleration * Time
                Velocity = prevPoint.GetVelocity() + (prevPoint.GetAcceleration() + Acceleration) * 0.5f * timeIncrement;
                // Set the New position to be the previous position + the mean(average) velocity * Time
                Position = prevPoint.GetPosition() + (prevPoint.GetVelocity() + Velocity) * 0.5f * timeIncrement;
                // calculate the acceleration due to gravity at the newly calculated position.
                Acceleration = gravMan.getGravPotentialAtPoint(prevPoint.GetPosition(), projectedTime);
            }
        }
    }
    
}

