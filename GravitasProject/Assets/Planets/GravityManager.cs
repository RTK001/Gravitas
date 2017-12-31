using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    // Create Singleton
    public static GravityManager instance = null;

    // Create list of Gravity sources (Heavy planets) in the scene
    private List<GravitySource> planetsList;

    // Gravitational constant - consider as calibration factor for force
    public float GravitationalConstant = 0.001f;

    public int numberOfPlanetsToCreate;
    public GameObject planetPrefab;
    public Vector3 [] planetPositions;
    public Vector3 [] planetrotationCentres;
    public float[] planetRotationalSpeeds;

    public void CreatePlanet(Vector3 position, Vector3 rotationCentre, float rotationSpeed)
    {
        GameObject plan = Instantiate(planetPrefab);        // Create the Planet
        plan.transform.position = position;       // set the planet to the desired position
        plan.GetComponent<PlanetSpin>().centreOfRotation = rotationCentre;        // assign the relevant rotation properties tot he PlanetSpin component
        plan.GetComponent<PlanetSpin>().angularSpeed = rotationSpeed;
        planetsList.Add(plan.GetComponent<GravitySource>());
    }



    public Vector3 getGravPotentialAtPoint (Vector3 point)
    {
        // Function to get the resultant gravitational potential on an object
        // Potential here is the gravity force divided by the ship's mass

        // Initialise result vector
        Vector3 resultant = new Vector3(0,0,0);

        // Apply the gravity force from each planet individually
        foreach (GravitySource Planet in planetsList)
        {
            // get the relative position from the point to the gravity source
            // this is done by getting the position of each planet and vector subtracting the coordinates of the ship
            Vector3 vecPointToPlanet = Planet.GetComponent<Transform>().position - point;

            // Add the planet's gravitational contribution to the resultant
            resultant += (Planet.Mass  * vecPointToPlanet) / vecPointToPlanet.sqrMagnitude;

        }
        
        return resultant * GravitationalConstant;
    }

    public Vector3 getGravPotentialAtPoint(Vector3 point, float time)
    {

        // Function to get the resultant gravitational potential on an object
        // Potential here is the gravity force divided by the ship's mass

        // Initialise result vector
        Vector3 resultant = new Vector3(0, 0, 0);
        float deltaTime = time - Time.time;

        // Apply the gravity force from each planet individually
        foreach (GravitySource Planet in planetsList)
        {
            // get the relative position from the point to the gravity source
            // this is done by getting the position of each planet and vector subtracting the coordinates of the ship
            Vector3 vecPointToPlanet = Planet.GetComponent<PlanetSpin>().GetPositionAtTime(deltaTime) - point;

            // Add the planet's gravitational contribution to the resultant
            resultant += (Planet.Mass  * vecPointToPlanet) / vecPointToPlanet.sqrMagnitude;

        }

        return resultant * GravitationalConstant;
    }

    void Awake()
    {
        if (instance == null)               // If the static value is null, set it to be this awakened instance.
        {
            instance = this;
        }
        else if (instance != null)          // if the value has been set to another instance of Playership, destroy this instance and preserve the other one.
        {
            Destroy(gameObject);
        }

        planetsList = new List<GravitySource>(FindObjectsOfType<GravitySource>());        // Initialise Planets List with each heavy object in the scene

        // As Planet creation and Planets array setting are used to create Trajectory Points, these have been placed in the Awake function to ensure they are done beforehand.
        // create each planet. 
        for (int i = 0; i > numberOfPlanetsToCreate; i++)
        {
            CreatePlanet(planetPositions[i], planetrotationCentres[i], planetRotationalSpeeds[i]);
        }

    }

}
