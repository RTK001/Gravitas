using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShip : Ship {

    // create a static Playership called instance, set to null as a default. This is used to ensure this class is a singleton.
    public static PlayerShip instance = null;

    // Energy related properties
    public int energyCapacity;
    EnergyManager energyMan;
    
    

    // Parameters relating to engine efficiency
    public int translationEnergyCost, rotationEnergyCost;
    public float forceAdditionRate, torqueAdditionRate;




    void GetKeyboardInputs()
    {
        // Function to get the Keyboard inputs, then call the relevant functions if required

        // A set of variables relating to the current keyboard inputs, and converts them to ints to calcualte net values
        int forward = System.Convert.ToInt32(Input.GetKey(KeyCode.W)) - System.Convert.ToInt32(Input.GetKey(KeyCode.S));        // Forward is the net forward - ie/  forward input -  backward input.

        int strafeRight = System.Convert.ToInt32(Input.GetKey(KeyCode.D)) - System.Convert.ToInt32(Input.GetKey(KeyCode.A));    // strafeRight is the net right - ie/  right -  left.

        int rotRight = System.Convert.ToInt32(Input.GetKey(KeyCode.E)) - System.Convert.ToInt32(Input.GetKey(KeyCode.Q));       // Net rotation

        if ((forward * forward) > 0 || (strafeRight * strafeRight) > 0 || (rotRight * rotRight) > 0)                    // if any of the net keyboard inputs are greater than 0,
        {
            int energyCost = translationEnergyCost * (forward*forward + strafeRight*strafeRight) + rotationEnergyCost * (rotRight * rotRight);           // calculate the total energy cost of the move

            if (energyMan.Subtract(energyCost, true))                           // if the energy cost was successfully paid (with safeguards as moving should not risk destroying the ship)
            {
                Move(forward, strafeRight, rotRight);                           // Call the move function of the ship

            }
            else
            {
                Debug.Log("Out of Energy!");                                     // while testing and debugging, alert console if out of energy.
            }
        }
    }



    // Ensure there is only one PlayerShip by creating a singleton
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
 
        DontDestroyOnLoad(gameObject);

        // Set setup energy manager
        energyMan = new EnergyManager(energyCapacity);

    }




    // Update is called once per frame
    void Update () {

        GetKeyboardInputs();

    }
}
