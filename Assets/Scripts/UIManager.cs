using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI ammoTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ammoTextMesh.text = player.GetComponent<ShootBullet>().ammo + "/" + player.GetComponent<ShootBullet>().maxAmmo;
    }
}
