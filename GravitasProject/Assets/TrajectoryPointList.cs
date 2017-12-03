using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPointList  {

    // A class to contain the Trajectory Points and ensure they coallate data correctly

    List <TrajectoryPoint> trajectoryPoints;        // List of the Trajectory Points
    List <Vector3> pointsToPlot;                    // Vector of Points to plot for easy access by TrajectoryUI
    int pointsToPlotIndex = 0;                      // an Index to remember the last Point to Plot updated between function calls
    public List <Vector3> GetPointsToPlot() { return pointsToPlot; }        // A getter function for PointsToPlot, required by the TrajectoryUI

    GravityManager gravMan;                 // The Gravity Manager passed to each TrajectoryPoint on initiation. Required to calculate ship trajectory.
    Transform playerTransform;              // The playership Transform - could theoretically be any transform with a rigid body
    Rigidbody rigid;                        // the rigid component of the transform

    int numberOfPointsToPlot;          // number of points to plot.




    void AddToPlotList(Vector3 pointToPlot)
    {
        // Adds TrajectoryPointData to the list of Points to plot
        pointsToPlot[pointsToPlotIndex] = pointToPlot;

        if (pointsToPlotIndex < pointsToPlot.Count - 1)   // If index can be safely increased without trying exceeding the array length
        {
            pointsToPlotIndex++;                            // Safely increase it
        }
        else                                                // if not
        {
            pointsToPlotIndex = 0;                          // Reset it to the beginning of the array
        }

    }


    public IEnumerator TrajectoryPointUpdateCoroutine()
    {   // A coroutine to handle Point calculation updating
        for (; ; )  // Continue repeating
        {
            foreach (TrajectoryPoint point in trajectoryPoints)     // Iterate through the TrajectoryPoints
            {
                point.CalculatePoint();                                         // Update the point
                AddToPlotList(point.GetPosition());                             // Add it to the plot list

                // yield and wait for a short amount of time:
                    // Not so long that the ship overtakes the points
                    // but not so short that the trajectory being calculated is too far in the future
                yield return new WaitForSeconds(point.GetTimeIncrement() /30);  

            }

            // Reset the first two trajectoryPoints in the array to the current player/object position, to then update the list from the start.
            // This is done periodically to compensate for calculation errors, collision and player input.
            Vector3 acc = gravMan.getGravPotentialAtPoint(playerTransform.position);        // Made a temporary variable to save this expensive operation being called twice.
            trajectoryPoints[0].SetEqual(playerTransform.position, rigid.velocity, acc);    // Set point 0 equal to playerTransform
            trajectoryPoints[1].SetEqual(playerTransform.position, rigid.velocity, acc);    // Set point 1 equal to point 0 as an initial guess.
        }
    }



    public TrajectoryPointList(GravityManager grav, PlayerShip playerShip, int noOfPoints)
    {   // Constructor Function
        

        gravMan = grav;                              // Needs to be passed the Gravity Manager, as this does not inherit from MonoBeahviour and cannot access FindObjectOfType<>()
        playerTransform = playerShip.transform;      // Needs to be passed the playership (though this could be modified to be any object with a transform and rigid components)
        rigid = playerShip.GetComponent<Rigidbody>();
        numberOfPointsToPlot = noOfPoints;           // Also is passed number of points, to make it accessible to the UI

        // Initialise the two lists
        trajectoryPoints = new List<TrajectoryPoint>();     
        pointsToPlot = new List<Vector3>();

        // Add point 1 to the TrajectoryPoint list. This uses a different constructor as there will be no previous point to pass it.
        trajectoryPoints.Add(new TrajectoryPoint(playerTransform.position, rigid.velocity, gravMan.getGravPotentialAtPoint(playerTransform.position), gravMan));

        for (int i = 1; i < numberOfPointsToPlot; i++)      // For each point to plot
        {
            trajectoryPoints.Add(new TrajectoryPoint(trajectoryPoints[i - 1]));         // Add a new point to the list, passing it the previous point
            pointsToPlot.Add(trajectoryPoints[i].GetPosition());                        // Add the point's position to the pointsToPlot list
        }


    }


}
