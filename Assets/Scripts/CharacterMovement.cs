using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] float MovementSpeed;
    [SerializeField] float JumpForce; 
    [SerializeField] int JumpCount;
    [SerializeField] bool isGrounded; 
    Rigidbody myRigidbody;

    float xValue; 
    SpriteRenderer spriteRenderer;
    Animator myAnimator;
    public Animator CameraAnimator; 
    
    
     
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();  
        JumpCount = 1; 
        isGrounded = true; 
        xValue = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        Movement(); 
    }

    void Movement(){

        //Movement
        isGrounded = true;
        xValue = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
        transform.Translate (xValue,0,0);
        myAnimator.SetFloat("Speed",Mathf.Abs(xValue));
        
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && JumpCount==1){
            isGrounded = false;
            myAnimator.SetBool("isJumping",true); 
            myRigidbody.AddForce(Vector3.up * JumpForce);
            JumpCount--;   
        } else {
            isGrounded = true;
        }

        //Flipping sprite
         if(xValue<0){
            spriteRenderer.flipX = true;
        } else if(xValue>0){
            spriteRenderer.flipX = false; 
        } 

    }

    void OnCollisionEnter(Collision other) {
        //Reset Jump Count when touching a platform
        switch(other.gameObject.tag){

            case "Platform":
            JumpCount = 1;
            isGrounded = true;
            myAnimator.SetBool("isJumping",false);
            break;

        }
    }

    void OnTriggerStay(Collider Cother) {

        switch(Cother.gameObject.tag){

            case "PerspectiveChangeZone":
            ChangeCameraView();
            break;

        }
    }

    void OnTriggerExit(Collider otherC) {
        
        switch(otherC.gameObject.tag){

            case "PerspectiveChangeZone":
            CameraAnimator.SetBool("IsCamera3D",false);
            break;

        }
    }



    void ChangeCameraView(){
        
        if(Input.GetKeyDown(KeyCode.F) && CameraAnimator.GetBool("IsCamera3D") == false ){
            CameraAnimator.SetBool("IsCamera3D",true);
        } else if (Input.GetKeyDown(KeyCode.F) && CameraAnimator.GetBool("IsCamera3D") == true){
            CameraAnimator.SetBool("IsCamera3D",false);
        }

        

    }


}