using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    private RaycastHit bulletHit;
    public int ammo;
    public int maxAmmo;

    private GameObject player;
    protected GameObject eventManager;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = 5;
        ammo = 5;
        eventManager = GameObject.Find("EventSystem");
        player = GameObject.Find("Player");
    }

void Update()
    {
        if (Time.timeScale > 0 && Input.GetMouseButtonDown(0) && ammo > 0)
        {
            //Debug.Log("shoot!");
            ammo--;
            eventManager.GetComponent<UIManager>().UpdateText();
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out bulletHit, 1000))
            {
                //Debug.Log("target found");

                if (bulletHit.rigidbody != null)
                {
                    bulletHit.transform.Translate(player.transform.forward, Space.World);
                    bulletHit.collider.GetComponentInParent<Target>().TargetHit();
                }                
            }
        } //else if (Input.GetMouseButtonDown(0) && ammo <= 0)
        //{
            //Debug.Log("out of ammo!");
        //}
    }

    public void Reload()
    {
        ammo = maxAmmo;
        GameObject.Find("EventSystem").GetComponent<UIManager>().UpdateText();
    }
}
