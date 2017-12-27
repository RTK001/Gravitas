using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOnEscape : MenuFunctions {

    public GameObject mainMenuPanel;
    public GameObject outOfEnergyPanel;
    public GameObject gameWonPanel;

    void Start()
    {
        mainMenuPanel = GameObject.Find("MainMenuPanel");
        if (mainMenuPanel.activeSelf)
        {
            ShowHideMenu(mainMenuPanel);
        }

        if (!outOfEnergyPanel)
        {
            outOfEnergyPanel = GameObject.Find("OutOfEnergyPanel");
        }

        if (!gameWonPanel)
        {
            gameWonPanel = GameObject.Find("GameWonPanel");
        }
        if (gameWonPanel.activeSelf)
        {
            ShowHideMenu(gameWonPanel);
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape) && !outOfEnergyPanel.activeSelf && !gameWonPanel.activeSelf)   // If esc is pressed AND the out of energy panel is not active
        {
            ShowHideMenu(mainMenuPanel);
        }
	}
}
