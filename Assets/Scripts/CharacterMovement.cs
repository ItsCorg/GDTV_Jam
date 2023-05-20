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
   // Animator myAnimator; 
    
    
     
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       // myAnimator = GetComponent<Animator>();  
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
        //myAnimator.SetBool("isRunning",false);
        //myAnimator.SetBool("isJumping",false);

        
        
        
        
        //Jumping
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && JumpCount==1){
            isGrounded = false;
           // myAnimator.SetBool("isJumping",true); 
            myRigidbody.AddForce(Vector3.up * JumpForce * Time.deltaTime);
            JumpCount--;   
        }

        //Flipping sprite
         if(xValue<0){
            spriteRenderer.flipX = true;
            //myAnimator.SetBool("isRunning",true);
             
        } else if(xValue>0){
            spriteRenderer.flipX = false;
            //myAnimator.SetBool("isRunning",true); 
            
        } 

    }

    void OnCollisionEnter(Collision other) {
        //Reset Jump Count when touching a platform
        if(other.gameObject.tag == "Platform"){
            JumpCount = 1;
            isGrounded = true;
              
        }
    }
}
