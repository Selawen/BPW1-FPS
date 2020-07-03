using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    protected GameObject target;
    public float destroyDelay;
    public int health;
    public int maxSpawnCount;
    public int pointsWorth;

    protected GameObject eventManager;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        target = this.gameObject;
        eventManager = GameObject.Find("EventSystem");
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
            eventManager.GetComponent<Score>().AddPoints(pointsWorth);
            eventManager.GetComponent<UIManager>().UpdateText();
        }
    }
}
