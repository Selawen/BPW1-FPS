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
    public TextMeshProUGUI timeTextMesh;
    public TextMeshProUGUI gameOverTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameOverScript = GetComponent<GameOverScript>();
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
            ammoTextMesh.color = new Color(0.09611962f, 0.2451054f, 0.754717f, 1);
        }

        ammoTextMesh.text = player.GetComponent<ShootBullet>().ammo + "/" + player.GetComponent<ShootBullet>().maxAmmo;
        pointsTextMesh.text = "Score: " + GetComponent<Score>().points;
    }

    public void GameOverText(string reason)
    {
        gameOverTextMesh.text = "<b>" + reason +"</b><br><br>" +
            "Score: " + GetComponent<Score>().points + "<br>"
                                + "Targets Killed: " + GetComponent<Score>().targetsKilled;
    }
}
