using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager
{
    // A class to handle Energy for a given object.
    // It should track the energy and perform relevant checks.


    public int maxEnergy = 0;   // The maximum energy the object can have when on a "full tank"
    private int currentEnergy = 0;  // The current energy

    // Default constructor
    public EnergyManager()
    {
        if (maxEnergy == 0)             // Alert if no energy capacity has been specified
        {
            Debug.Log("Please specify a maximum energy capacity");
        }

    }

    // Constructor with arguements
    public EnergyManager(int EnergyCapacity)
    {
        maxEnergy = EnergyCapacity;             // set the energy capacity and current energy based on the input arguement.
        currentEnergy = maxEnergy;
    }

    public bool Subtract(int energyCost, bool safeguards)
    // This function adds relevant checks to the energy value before changing it, such as checking capacity, etc.
    // This will likely be called by the ship's engine when energy is used for propulsion. Hence, there would likely be safeguards to prevent energy going below 0
    // As Energy is a substitute for health, this function can be called by something external with intent to destroy the object. THis would be called without safeguards.
    {
        // First check the energy available. If this energy cost can be afforded
        if (energyCost <= currentEnergy)
        {
            currentEnergy -= energyCost;        // subtract the energy cost from the current energy
            return true;                        // the energy cost has been paid, so this function has been successfully called.
        }

        // If there is not sufficient energy to afford the transaction
        else
        {
            if (safeguards == false)     // This will bring the object below 0 energy, destroying it.
            {

                // this.Destroy();      // Run the destroy script for the object.
                return true;           // The cost has been paid, but destroyed the owning object in the process.
            }
            else                        // If there are safeguards, the cost will not be paid.
            {
                return false;           // The cost has not been paid - this function will return false.
            }
        }

    }

    public bool Add(int energyToAdd, bool canExceedFull)
    // There will likely be items or situations where energy can be added.
    {
        // if energy can be safely added without approaching the energy capacity limit
        if ((canExceedFull == true) || (energyToAdd + currentEnergy < maxEnergy))
        {
            currentEnergy += energyToAdd;       // add the energy
            return true;                        // This enegry has been added successfully, so return true.
        }

        // as the first if statement is not true, this function will bring the energy to it's limit.
        else if (currentEnergy != maxEnergy)    // check if the energy is not already at it's limit.
        {
            currentEnergy = maxEnergy;          // if it isn't, bring it to it's limit.
            return true;                        // energy was successfully added, so return true.
        }

        else
        {
            return false;                       // if the energy is already at it's limit, return false. 0 energy was added.
        }
    }

    // A function to get the current Energy Value
    int getEnergy()
    {
        return currentEnergy;
    }



}