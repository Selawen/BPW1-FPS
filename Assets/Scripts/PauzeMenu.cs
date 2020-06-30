using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauzeMenu : MonoBehaviour
{
    //boolean to change settings necessairy to pause
    private bool GamePaused = false;
    private GameObject pauseMenu;
    private GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        howToPlayPanel = GameObject.Find("HowToPlay");
        pauseMenu.SetActive(false);
        howToPlayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// toggle the pause menu, timeScale and cursorLockMode
    /// </summary>
    public void TogglePause()
    {
        GamePaused = !GamePaused;

        //pause gameplay when the game is paused and start it again when the game is resumed
        Time.timeScale = (!GamePaused ? 1 : 0);
        //lock cursor during gameplay, unlock when paused
        Cursor.lockState = (GamePaused ? CursorLockMode.None : CursorLockMode.Locked);

        pauseMenu.SetActive(GamePaused);
    }

    /// <summary>
    /// buttons
    /// </summary>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void HowToPlay(bool setActive)
    {
        howToPlayPanel.SetActive(setActive);
    }
}
