using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    private GameObject howToPlayPanel;

    // Start is called before the first frame update
    void Start()
    {
        howToPlayPanel = GameObject.Find("HowToPlay");
        howToPlayPanel.SetActive(false);
    }

    public void HowToPlay(bool setActive)
    {
        howToPlayPanel.SetActive(setActive);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
