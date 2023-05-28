using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLauncher : MonoBehaviour
{
    public ParticleSystem sparkParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
     public void SparkPlay() {
        sparkParticle.Play();
    }

    public void SparkStop() {
        sparkParticle.Stop();
    }
}
