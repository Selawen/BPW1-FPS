using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public float gameOverTimer;
    private GameObject gameOverPanel;
    bool newHighscore;

    // Start is called before the first frame update
    void Start()
    {
        if (gameOverTimer == 0)
        {
            gameOverTimer = 30;
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
        Cursor.visible = true;

        if (GetComponent<Score>().points > PlayerPrefs.GetInt("highscore", 0))
        {
            newHighscore = true;
            PlayerPrefs.SetInt("highscore", GetComponent<Score>().points);
            PlayerPrefs.Save();
        } else { newHighscore = false; }

        GetComponent<UIManager>().GameOverText(reason, newHighscore);
    }
}
