using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public AudioSource checkpointSource;
    Transform myTransform;
    Animator myAnimator; 
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponentInChildren<Transform>();
        myAnimator = GetComponentInChildren<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider col){
         
        if(col.gameObject.tag == "Player"){
            myAnimator.SetBool("IsCheckPointCleared",true); 
            if (checkpointSource != null)
            {
                checkpointSource.Play();
                checkpointSource = null;
            }
        }
    }
}
