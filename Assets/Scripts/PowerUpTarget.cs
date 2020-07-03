using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTarget : Target
{
    public GameObject powerupTrigger;


    public override void TargetHit()
    {
        base.TargetHit();
        if (health == 0)
        {
            Instantiate(powerupTrigger, target.transform.position + new Vector3(0,0,0), target.transform.rotation);
        }
    }
}
