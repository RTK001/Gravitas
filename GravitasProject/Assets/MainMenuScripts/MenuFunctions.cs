using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {


    public void ShowHideMenu(GameObject item)
    // Shows or hides the main menu panel
    {
        // If the panel is hidden, unhide it. If it's shown, hide it.
        // The game is also paused using Time.timescale.
        if (item.activeSelf)
        {
            item.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            item.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGravitasGame()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        Application.Quit();
    }

    public void RestartLevel()
    {   // Called to re-load the level
        Time.timeScale = 1;             // Restart time
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);    // load the current scene again
    }

    public void GoToMainMenu()
    {   // called to go to the main menu
        SceneManager.LoadScene("Main Menu Scene");
    }

}
