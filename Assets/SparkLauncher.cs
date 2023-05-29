using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkLauncher : MonoBehaviour
{
    public ParticleSystem sparkParticle;
     public AudioClip animationStartSound;
     public AudioClip puff; 
     public AudioSource soundEffect; 
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

 public void KissSound() {
        // Play the sound effect at the beginning of the animation
            soundEffect.clip = animationStartSound;
            soundEffect.Play();
    }

 public void Puff() {
        // Play the sound effect at the beginning of the animation
            soundEffect.clip = puff;
            soundEffect.Play();
    }
    

}
