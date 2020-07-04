using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public float gameOverTimer;
    private GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (gameOverTimer == 0)
        {
            gameOverTimer = 60;
        }

        gameOverPanel = GameObject.Find("GameOverScreen");
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOverTimer <= 0)
        {
            GameOver("Time's up!");
        }
        else
        {
            gameOverTimer -= 0.02f;
        }
    }

    public void GameOver(string reason)
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<UIManager>().GameOverText(reason);
    }
}
