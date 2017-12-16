using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour {


    public void ShowHideMenu(GameObject item)
    // Shows or hides the main menu panel
    {
        // If the panel is hidden, unhide it. If it's shown, hide it.
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

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu Scene");
    }

}
