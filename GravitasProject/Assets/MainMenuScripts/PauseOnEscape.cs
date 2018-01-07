using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEscape : MenuFunctions {


    public List<GameObject> panels;

    public GameObject defaultManiMenuPanel;     // The panel to default show or hide when escape is pressed
    public GameObject gameWonPanel;             // The panel to subscribe to OnGoalComplete

    bool AllPanelsInactive()
    {
        foreach(GameObject panel in panels)
        {
            if (panel.activeSelf)
            {
                return false;
            }
        }
        return true;
    }


     GameObject GetActivePanel()
    {
        foreach (GameObject panel in panels)
        {
            if (panel.activeSelf)
            {
                return panel;
            }
        }
        return null;
    }

    void ShowHideGameWonPanel(int currentGoal, int totalGoals)
    {
        if (currentGoal == totalGoals)
        {
            ShowHideMenu(gameWonPanel);
        }
    }
    

    void Start()
    {
        // Initialise panel show/hide subscriptions here

        GoalManagerScript.OnGoalComplete += ShowHideGameWonPanel;
        

        // Hide all panels
        foreach (GameObject panel in panels)
        {
            if (panel.activeSelf)
            {
                ShowHideMenu(panel);
            }
        }

        

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (AllPanelsInactive() )  // If all panels are inactive
            {
                
                ShowHideMenu(defaultManiMenuPanel);    // Escape will show/hide the default Main Menu Panel
            }
            else
            {
                ShowHideMenu(GetActivePanel());     // If a different panel is active, escape will show/hide the active panel
            }
        }
        
	}
}
