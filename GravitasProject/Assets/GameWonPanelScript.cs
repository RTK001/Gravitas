using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonPanelScript : MonoBehaviour {

    MenuFunctions menu;


	// Use this for initialization
	void Start () {

        menu = this.GetComponent<MenuFunctions>(); 

        GoalManagerScript.OnGameWin += ShowHideGameWonPanel;
		
	}
	
    void ShowHideGameWonPanel()
    {
        menu.ShowHideMenu(this.gameObject);
    }

}
