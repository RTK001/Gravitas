using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonPanelScript : MonoBehaviour {

    MenuFunctions menu;


	// Use this for initialization
	void Start () {

        menu = this.GetComponent<MenuFunctions>(); 

        GoalManagerScript.OnGoalComplete += ShowHideGameWonPanel;
		
	}
	
    void ShowHideGameWonPanel(int currentGoal, int totalGoals)
    {

        if (currentGoal == totalGoals)
        {
            menu.ShowHideMenu(gameObject);
        }
        
    }

}
