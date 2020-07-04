using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

        //forward *= Time.deltaTime;
        //sideways *= Time.deltaTime;

        Vector3 newPosition = goPlayer.transform.position + (goPlayer.transform.forward * forward * Time.fixedDeltaTime + goPlayer.transform.right * sideways * Time.fixedDeltaTime);
        newPosition.x = Mathf.Clamp(newPosition.x, -44, 35.3f);
        newPosition.z = Mathf.Clamp(newPosition.z, -61, 36);
        //goPlayer.GetComponent<Rigidbody>().AddForce(transform.position + transform.forward * forward * Time.fixedDeltaTime);
        goPlayer.GetComponent<Rigidbody>().MovePosition(newPosition);
        //goPlayer.transform.Translate(0, 0, forward);
        //goPlayer.transform.Translate(sideways, 0, 0);

        //looking around with mouse
        float h = Input.GetAxis("Mouse X") * horizontalLook;
        float v = Input.GetAxis("Mouse Y") * verticalLook;

        vRotation -= v;

        vRotation = Mathf.Clamp(vRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(vRotation, 0f, 0f);
        goPlayer.transform.Rotate(0, h, 0);
    }
}
