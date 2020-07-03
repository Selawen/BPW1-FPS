using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI ammoTextMesh;
    public TextMeshProUGUI pointsTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
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
}
