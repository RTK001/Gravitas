using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Script : MonoBehaviour {

    MenuFunctions playerCanvasMenu;
    public GameObject userInfoPanel;
    Text userInfoPanelText;
    GravityManager gravMan;
    PlayerShip playerShip;
    EnemyManager enemyMan;

    // Parameters for Planetary Slingshot
    public float PlanetDistanceRight = 60;  // Z location of planet starting point, relative to player ship
    public float playerPlanetGap = 10;      // Intended gap between the planet and player when slingshot occurs
    

    void OnLevelGoal (int currentGoal, int totalGoals)
    {

        switch (currentGoal)
        {

            case 1:
                playerCanvasMenu.ShowHideMenu(userInfoPanel);       // Show the User Info panel
                playerShip.translationEnergyCost = 0;               // Disable the energy cost of movement for tutorial level
                userInfoPanelText.text = "Welcome to the Gravitas Tutorial level! \n In this game, you control a satellite. \n try moving it, using the W,A,S,D keys. \n";  // Set panel text
                break;


            case 2:
                playerCanvasMenu.ShowHideMenu(userInfoPanel);   // Show the User Info panel
                userInfoPanelText.text = "Excellent! \n The satellite can use the gravity of \n nearby planets to get a speed boost!\n";        // Set panel text

                // Planetary slingshot

                // Planet rotational speed
                // Calculated as double hte players velocity, converted into degrees/sec
                float planetRot = playerShip.GetComponent<Rigidbody>().velocity.x / PlanetDistanceRight * 180/3.141f * 2;   

                // X location of planet's centre of rotation, relative to player.
                // Calculated assuming the planet will rotate clockwise 90 degrees before arcing infront of the player.
                // IE/ the planet will travel 90 degrees (pi/2 rads) before it should swing infront of the player
                float distanceAhead = - (  (3.141f / 2) * PlanetDistanceRight - playerPlanetGap);

                Vector3 pos = playerShip.transform.position + new Vector3(-playerPlanetGap, 0, PlanetDistanceRight);
                Vector3 origin = playerShip.transform.position + new Vector3(distanceAhead,0, PlanetDistanceRight);
                gravMan.CreatePlanet(pos, origin, -planetRot);      // Create a planet as specified

                // Limit playerspeed to reduce issues
                playerShip.maxSpeed = playerShip.GetComponent<Rigidbody>().velocity.magnitude + 1;

                break;


            case 3:
                playerCanvasMenu.ShowHideMenu(userInfoPanel);       // Show the User Info panel
                userInfoPanelText.text = "Well done! \n Now try using it to avoid Enemies of \n Also watch your energy levels \n";      // Set panel text

                playerShip.translationEnergyCost = 1;       // Include player movement energy costs
                playerShip.maxSpeed = 100;                  // reset the allowable max speed

                enemyMan.SpawnPoint = new Vector3(-120, 0, 0);      // begin spawning three enemies in quick succession between the player and the goal
                enemyMan.spawnTime = 3;
                enemyMan.totalEnemies = 3;
                enemyMan.StartCoroutine("EnemyCoRoutine");

                Vector3 planetLoc = new Vector3(-100, 0, 0);        // Create a planet between the player and the enemies
                gravMan.CreatePlanet(planetLoc, planetLoc, 0);

                break;
        }

    }

    // Use this for initialization
    void Awake()
    {
        
        GoalManagerScript.OnGoalComplete += OnLevelGoal;
        playerCanvasMenu = GameObject.Find("Player Canvas").GetComponent<MenuFunctions>();
        // userInfoPanel = playerCanvasMenu.transform.FindChild("UserInfoPanel").gameObject;
        // userInfoPanel = FindObjectOfType<PauseOnEscape>().panels.Find(pan => pan.name == "UserInfoPanel");

        userInfoPanelText = userInfoPanel.GetComponentInChildren<Text>();
        gravMan = FindObjectOfType<GravityManager>();
        playerShip = FindObjectOfType<PlayerShip>();
        enemyMan = FindObjectOfType<EnemyManager>();

    }


}
