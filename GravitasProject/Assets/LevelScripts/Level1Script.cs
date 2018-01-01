using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1Script : MonoBehaviour {

    MenuFunctions playerCanvasMenu;
    GameObject userInfoPanel;
    Text userInfoPanelText;


    

    void OnLevelGoal (int currentGoal, int totalGoals)
    {
        switch (currentGoal)
        {

            case 1:
                playerCanvasMenu.ShowHideMenu(userInfoPanel);
                userInfoPanel.GetComponentInChildren<Text>().text = "Welcome to the Gravitas Tutorial level! \n In this game, you control a satellite. \n try moving it, using the W,A,S,D keys.";
                break;

        }

    }

    // Use this for initialization
    void Awake()
    {
        
        GoalManagerScript.OnGoalComplete += OnLevelGoal;
        userInfoPanel = GameObject.Find("UserInfoPanel");
        playerCanvasMenu = GameObject.Find("Player Canvas").GetComponent<MenuFunctions>();
    }


}
