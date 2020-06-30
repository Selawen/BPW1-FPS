using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTarget : Target
{
    public GameObject powerupTrigger;

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TargetHit()
    {
        base.TargetHit();
        if (health == 0)
        {
            Instantiate(powerupTrigger, target.transform.position + new Vector3(0,0,0), target.transform.rotation);
        }
    }
}
