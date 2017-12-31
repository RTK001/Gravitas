using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEscape : MenuFunctions {


    public List<GameObject> panels;

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


    void Start()
    {

        foreach(GameObject panel in panels)
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
            if (AllPanelsInactive() )  // If esc is pressed AND the out of energy panel is not active
            {
                ShowHideMenu(panels[0]);
            }
            else
            {
                ShowHideMenu(GetActivePanel());
            }
        }
		

        
	}
}
