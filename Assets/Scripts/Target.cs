using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected GameObject target;
    public float destroyDelay;
    public int health;
    public int maxSpawnCount;

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// decreases health upon hit
    /// </summary>
    public virtual void TargetHit()
    {
        health--;

        //destroy target when health is 0
        if (health == 0)
        {
            Destroy(target, destroyDelay);
        }
    }
}
