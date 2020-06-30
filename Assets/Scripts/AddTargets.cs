using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTargets : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Target"))
        {
            int maxSpawn = target.GetComponent<Target>().maxSpawnCount;
            for (int x = Random.Range(1, maxSpawn); x > 0; x--)
            {
                Instantiate(target, (Random.insideUnitSphere * Random.Range(1, 20)), target.transform.rotation);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
