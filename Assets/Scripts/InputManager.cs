﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject pause;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        pause = GameObject.Find("UICanvas");
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pause.GetComponent<PauzeMenu>().TogglePause();
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            player.GetComponent<ShootBullet>().Reload();
        }
    }
}
