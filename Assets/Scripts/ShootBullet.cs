using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    private RaycastHit bulletHit;
    public int ammo;
    public int maxAmmo;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = 5;
        ammo = 5;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            Debug.Log("shoot!");
            ammo--;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out bulletHit, 1000))
            {
                Debug.Log("target found");

                if (bulletHit.transform != null)
                {
                    bulletHit.transform.Translate(Camera.main.transform.forward);
                    bulletHit.collider.GetComponentInParent<Target>().TargetHit();
                }
            }
        } else if (Input.GetMouseButtonDown(0) && ammo <= 0)
        {
            Debug.Log("out of ammo!");
        }
    }

    public void Reload()
    {
        ammo = maxAmmo;
    }
}
