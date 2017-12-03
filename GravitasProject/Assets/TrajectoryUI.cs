using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TrajectoryUI : MonoBehaviour {

    // This class contains the UI for implementing a trajectory plotter. It should be added to objects that need their

    public GameObject spherePrefab;     // The sphere prefabs to place at each trajectory point

    public int numberOfPoints = 15;          // Made public for easy changing from the Unity Scene Editor
    
    GameObject[] UIPoints;              // An array of the spheres to iterate through

    TrajectoryPointList trajectoryPointList;        // A TrajectoryPointList class to handle the calculations

    


    public GameObject CreateSphere()
    {
        // Called on Start to instantiate the sphere prefabs
        GameObject sphere = Instantiate(spherePrefab);      // Instantiate the spheres
        sphere.transform.position = new Vector3(0, 0, 0);   // set to correct location
        return sphere;  
    }

   
    // Use this for initialization
    void Start()
    {
        // Initialise the TrajectoryPointList, passing the relevant objects.
        // These are required as TrajectoryPointList does not inherit from MonoBehaviour, and thus it cannot access FindObjectOfType<>();
        trajectoryPointList = new TrajectoryPointList(FindObjectOfType<GravityManager>(), FindObjectOfType<PlayerShip>(), numberOfPoints);

        
        // Instantiate UIPointsList as the length of the trajectoryPoints
        UIPoints = new GameObject[trajectoryPointList.GetPointsToPlot().Count];

        // Iterate through all the points created by the TrajectoryPointList constructor
        for (int i = 0; i < trajectoryPointList.GetPointsToPlot().Count; i++)
        {
            UIPoints[i] = CreateSphere();       // Instantiate a sphere prefab in the UIPoints list
        }

        // Start the calculations coroutine in the TrajectoryPointsList.
        // As it does not inherit from MonoBehaviour, coroutines must be started in this function, rather than in TrajectoryPointsList's constructor
        StartCoroutine(trajectoryPointList.TrajectoryPointUpdateCoroutine());       
    }


    // Update is called once per frame
    void Update () {

        for (int i=0; i < UIPoints.Length; i++)     // For each sphere
        {
            UIPoints[i].transform.position = trajectoryPointList.GetPointsToPlot()[i];      // set it's position to be the latest Plot to point, calculated by the TrajectoryPointsList

        }
        
    }
}
