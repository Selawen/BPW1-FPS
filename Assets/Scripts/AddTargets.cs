﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTargets : MonoBehaviour
{
    public List<GameObject> targetList { get; private set; } = new List<GameObject>();

    public int waveCounter { get; private set; }
    private int waveTimer;
    [SerializeField] private int waveDelay;
    [SerializeField] private float waveMultiplier;

    private Vector3 instantiatePos;
    [SerializeField] private Vector3 offset;
    private Collider[] colliderTest;
    private bool outOfRange;

    public int targetCounter;

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Target"))
        {
            targetList.Add(target);
            target.SetActive(false);
        }

        if (waveDelay == 0)
        {
            waveDelay = 10;
        }

        if (waveMultiplier == 0)
        {
            waveMultiplier = 1;
        }

        targetCounter = 0;
        waveCounter = 1;
        AddNewTargets(waveCounter);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewTargets(int wave)
    {
        foreach (GameObject target in targetList)
        {
            if (waveCounter >= target.GetComponent<Target>().startSpawningAtWave)
            {
                int maxSpawn = target.GetComponent<Target>().maxSpawnCount;

                for (int x = Random.Range(0, maxSpawn); x > 0; x--)
                {
                    do
                    {
                        instantiatePos = (Random.insideUnitSphere * Random.Range(1, 70)) + offset;
                        colliderTest = Physics.OverlapBox(instantiatePos, target.transform.localScale / 2, Quaternion.identity);

                        if (instantiatePos.x > 35 || instantiatePos.x < -43 || instantiatePos.z > 35 || instantiatePos.z < -60) {
                            outOfRange = true;
                        }
                        else { outOfRange = false; }

                    } while (colliderTest.Length > 0 || outOfRange);

                    GameObject newTarget = Instantiate(target, instantiatePos, Quaternion.Euler(0, Random.Range(0,360), 0));
                    //actually spawn target
                    newTarget.SetActive(true);

                    //set the height position as the same as the default
                    newTarget.transform.position = new Vector3(instantiatePos.x, target.transform.position.y, instantiatePos.z);
                    targetCounter++;
                }
            }
        }
        waveCounter++;
        waveTimer = waveDelay - (int)(wave * waveMultiplier);
        waveTimer = Mathf.Clamp(waveTimer ,1 , 30);
        StartCoroutine(WaveTimer());
    }
    
    IEnumerator WaveTimer()
    {
        while (waveTimer > 0) {
            waveTimer--;
        yield return new WaitForSeconds(1.0f);
        }
        AddNewTargets(waveCounter);
    }
}
