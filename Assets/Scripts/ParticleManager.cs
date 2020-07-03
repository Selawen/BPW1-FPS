using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// play particle effect
    /// </summary>
    /// <param name="healthLeft">amount of health left</param>
    public void HitParticles(ParticleSystem hitParticles, int healthLeft)
    {
        hitParticles.Stop(true);
        var main = hitParticles.main;
        main.duration = (3.5f - healthLeft);
        hitParticles.Play(true);
    }
}
