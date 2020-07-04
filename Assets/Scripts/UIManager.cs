using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    private GameOverScript gameOverScript;

    public TextMeshProUGUI ammoTextMesh;
    public TextMeshProUGUI pointsTextMesh;
    public TextMeshProUGUI highscoreTextMesh;
    public TextMeshProUGUI timeTextMesh;
    public TextMeshProUGUI gameOverTextMesh;
    public GameObject newHighscoreTextMesh;

    private Color defaultColour;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameOverScript = GetComponent<GameOverScript>();
        defaultColour = ammoTextMesh.color;
        UpdateText();
    }

    private void FixedUpdate()
    {
        timeTextMesh.text = "Time: " + (int)(gameOverScript.gameOverTimer / 60) + ":" + (int)(gameOverScript.gameOverTimer % 60);
    }

    public void UpdateText()
    {
        //change colour ammo text if out of ammo 
        if (player.GetComponent<ShootBullet>().ammo == 0)
        {
            ammoTextMesh.color = Color.red;
        } else {
            ammoTextMesh.color = defaultColour;
        }

        //change colour highscore text if new highscore
        if (GetComponent<Score>().points > PlayerPrefs.GetInt("highscore", 0))
        {
            highscoreTextMesh.color = Color.green;
        }
            
        ammoTextMesh.text = player.GetComponent<ShootBullet>().ammo + "/" + player.GetComponent<ShootBullet>().maxAmmo;
        pointsTextMesh.text = "Score: <b>" + GetComponent<Score>().points + "</b>";
        highscoreTextMesh.text = "Highscore: " + Mathf.Max(GetComponent<Score>().points, PlayerPrefs.GetInt("highscore", 0));
    }

    public void GameOverText(string reason, bool newHighscore)
    {
        newHighscoreTextMesh.SetActive(newHighscore);
        gameOverTextMesh.text = "<b>" + reason +"</b><br><br>" +
            "Score: " + GetComponent<Score>().points + "<br>"
                                + "Targets Killed: " + GetComponent<Score>().targetsKilled;
    }
}
