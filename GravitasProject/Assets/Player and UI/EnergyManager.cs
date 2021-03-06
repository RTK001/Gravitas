﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager
{
    // A class to handle Energy for a given object.
    // It should track the energy and perform relevant checks.


    public int maxEnergy = 0;   // The maximum energy the object can have when on a "full tank"
    private int currentEnergy =0 ;  // The current energy

    public UIManagerEnergyBar barUI;

    public GameObject outOfEnergyPanel;     // the Panel that alerts the player the game is ove,r but allows them to keep going.
    bool outOfEnergy;                       // To prevent alerting the player multipel times that they are out of energy.

    void PrintEnergy()
    {
        Debug.Log(currentEnergy);
    }


    // Default constructor
    public EnergyManager()
    {
        if (maxEnergy == 0)             // Alert if no energy capacity has been specified
        {
            Debug.Log("Please specify a maximum energy capacity");
        }

        barUI = Object.FindObjectOfType<UIManagerEnergyBar>();
        barUI.SetEnergyBar(1.0f);

    }

    void OutOfEnergy()
    {
        if (!outOfEnergyPanel.activeSelf && !outOfEnergy)
        {
            outOfEnergyPanel.GetComponent<MenuFunctions>().ShowHideMenu(outOfEnergyPanel);
            outOfEnergy = true;
        }
    }


    // Constructor with arguments
    public EnergyManager(int EnergyCapacity)
    {
        maxEnergy = EnergyCapacity;             // set the energy capacity and current energy based on the input arguement.
        currentEnergy = maxEnergy;

        barUI = Object.FindObjectOfType<UIManagerEnergyBar>();

        if (!outOfEnergyPanel)
        {
            outOfEnergyPanel = GameObject.Find("OutOfEnergyPanel");
        }
        outOfEnergyPanel.GetComponent<MenuFunctions>().ShowHideMenu(outOfEnergyPanel);
        outOfEnergy = false;

        barUI.SetEnergyBar(1.0f);
    }



    public bool Subtract(int energyCost)
    // This function adds relevant checks to the energy value before changing it, such as checking capacity, etc.
    // This will likely be called by the ship's engine when energy is used for propulsion. Hence, there would likely be safeguards to prevent energy going below 0
    // As Energy is a substitute for health, this function can be called by something external with intent to destroy the object. THis would be called without safeguards.
    {

        // First check the energy available. If this energy cost can be afforded
        if (energyCost <= currentEnergy)
        {
            currentEnergy -= energyCost;        // subtract the energy cost from the current energy

            barUI.SetEnergyBar(System.Convert.ToSingle(currentEnergy) / System.Convert.ToSingle(maxEnergy));        // Set the energy Bar to reflect energy levels

            return true;                        // the energy cost has been paid, so this function has been successfully called.
        }

        // If there is not sufficient energy to afford the transaction
        else
        {  
             OutOfEnergy();
             return false;           // The cost has not been paid - this function will return false.     
        }
        
    }



    public bool Add(int energyToAdd, bool canExceedFull)
    // There will likely be items or situations where energy can be added.
    {


        PrintEnergy();


        // if energy can be safely added without approaching the energy capacity limit
        if ((canExceedFull == true) || (energyToAdd + currentEnergy < maxEnergy))
        {
            currentEnergy += energyToAdd;       // add the energy

            barUI.SetEnergyBar(System.Convert.ToSingle(currentEnergy) / System.Convert.ToSingle(maxEnergy));        // Set the energy Bar to reflect energy levels

            return true;                        // This enegry has been added successfully, so return true.
        }

        // as the first if statement is not true, this function will bring the energy to it's limit.
        else if (currentEnergy != maxEnergy)    // check if the energy is not already at it's limit.
        {
            currentEnergy = maxEnergy;          // if it isn't, bring it to it's limit.

            barUI.SetEnergyBar(System.Convert.ToSingle(currentEnergy) / System.Convert.ToSingle(maxEnergy));        // Set the energy Bar to reflect energy levels

            return true;                        // energy was successfully added, so return true.
        }

        else
        {
            return false;                       // if the energy is already at it's limit, return false. 0 energy was added.
        }
    }




    // A function to get the current Energy Value
    int GetEnergy()
    {
        return currentEnergy;
    }

 
}