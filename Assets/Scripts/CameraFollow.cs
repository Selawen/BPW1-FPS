using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 10.0f;
    public float horizontalLook = 2.0f;
    public float verticalLook = 2.0f;
    public float vRotation;
    public GameObject goPlayer;

    void Start()
    {
        goPlayer = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Locked;
    }


    void FixedUpdate()
    {
        //movement over plane
        float forward = Input.GetAxis("Vertical") * speed;
        float sideways = Input.GetAxis("Horizontal") * speed;

        forward *= Time.deltaTime;
        sideways *= Time.deltaTime;

        goPlayer.transform.Translate(0, 0, forward);
        goPlayer.transform.Translate(sideways, 0, 0);

        //looking around with mouse
        float h = Input.GetAxis("Mouse X") * horizontalLook;
        float v = Input.GetAxis("Mouse Y") * verticalLook;

        vRotation -= v;

        vRotation = Mathf.Clamp(vRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(vRotation, 0f, 0f);
        goPlayer.transform.Rotate(0, h, 0);
    }
}
