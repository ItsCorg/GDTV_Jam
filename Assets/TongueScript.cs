using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour
{
    public CharacterMovement characterMovement;
    Animator TongueAnimator;
    float StrechValue;

    public GameObject Frog;
    public GameObject Obstacle;
    Vector3 FrogToObstaclePos;  
    // Start is called before the first frame update
    void Start()
    {
        TongueAnimator = GetComponent<Animator>();
        FrogToObstaclePos = new Vector3(Frog.transform.position.x / Obstacle.transform.position.x, Frog.transform.position.y / Obstacle.transform.position.y,Frog.transform.position.z / Obstacle.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        RollTongue(); 
    }

    void RollTongue(){
        if(!characterMovement.canSwitchCamera){
            return; 
        }

        if(characterMovement.myAnimator.GetBool("IsCamera3D") == true && TongueAnimator.GetBool("RollTongueOut") == false){

            TongueAnimator.SetBool("RollTongueOut",true); 

        } else if (characterMovement.myAnimator.GetBool("IsCamera3D") == false && TongueAnimator.GetBool("RollTongueOut") == true){

            TongueAnimator.SetBool("RollTongueOut",false); 
        }
    }
}
