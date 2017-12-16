using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEscape : MenuFunctions {

    public GameObject mainMenuPanel;

    void Start()
    {
        // mainMenuPanel = GameObject.Find("MainMenuPanel");
        if (mainMenuPanel.activeSelf)
        {
            ShowHideMenu(mainMenuPanel);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape)==true)
        {
            ShowHideMenu(mainMenuPanel);
        }
	}
}
