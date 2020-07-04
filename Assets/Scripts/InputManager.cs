using System.Collections;
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
        //pause when esc is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pause.GetComponent<PauzeMenu>().TogglePause();
        }

        //reload with R
        if (Input.GetKeyUp(KeyCode.R))
        {
            player.GetComponent<ShootBullet>().Reload();
        }

        //double speed if left shift is pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            player.GetComponentInChildren<PlayerMovement>().speed = 20.0f;
        }
        else
        {
            player.GetComponentInChildren<PlayerMovement>().speed = 10.0f;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            PlayerPrefs.SetInt("highscore", 0);
            PlayerPrefs.Save();
        }
    }
}
