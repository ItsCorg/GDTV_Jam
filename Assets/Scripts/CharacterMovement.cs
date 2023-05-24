using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

  [SerializeField] float MovementSpeed;
  [SerializeField] float JumpForce;
  [SerializeField] int JumpCount;
  [SerializeField] bool isGrounded;
  Rigidbody myRigidbody;

  float xValue;
  SpriteRenderer spriteRenderer;
  public Animator myAnimator;
  //public Animator CameraAnimator;
  public CameraController cameraController;


  // Start is called before the first frame update
  void Start() {
    myRigidbody = GetComponent<Rigidbody>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    myAnimator = GetComponent<Animator>();
    JumpCount = 1;
    isGrounded = true;
    xValue = 0;
  }

  // Update is called once per frame
  void Update() {
    Movement();
    ChangeCameraView();
  }

  void Movement() {

    if (!cameraController.IsCamera3D) {
      //Movement
      isGrounded = true;
      xValue = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
      transform.Translate(xValue, 0, 0);
      myAnimator.SetFloat("Speed", Mathf.Abs(xValue));

      //Jumping
      if (Input.GetKeyDown(KeyCode.Space) && isGrounded && JumpCount == 1) {
        isGrounded = false;
        myAnimator.SetBool("isJumping", true);
        myRigidbody.AddForce(Vector3.up * JumpForce);
        JumpCount--;
      } else {
        isGrounded = true;
      }

      //Flipping sprite
      if (xValue < 0) {
        // flipX in spriteRenderer: Only the rendering is affected. Use negative Transform.scale, if you want to affect all the other components (for example colliders).
        //spriteRenderer.flipX = false;
      } else if (xValue > 0) {
        //spriteRenderer.flipX = true;
      }

      if (xValue > 0 && !flipped) {
        FlipRight();
      }
      if (xValue < 0 && flipped) {
        FlipLeft();
      }

    } else {
      myAnimator.SetFloat("Speed", 0);
    }
  }

  // this can be done easier 
  bool flipped = false;
  void FlipRight() {
    flipped = true;
    var s = transform.localScale; s.x *= -1;
    transform.localScale = s;
  }
  void FlipLeft() {
    flipped = false;
    var s = transform.localScale; s.x = Mathf.Abs(s.x);
    transform.localScale = s;
  }

  void OnCollisionEnter(Collision other) {
    //Reset Jump Count when touching a platform
    switch (other.gameObject.tag) {

      case "Platform":
        JumpCount = 1;
        isGrounded = true;
        myAnimator.SetBool("isJumping", false);
        break;

    }
  }

  public bool canSwitchCamera = false;
  void OnTriggerEnter(Collider Cother) {

    switch (Cother.gameObject.tag) {
      case "PerspectiveChangeZone":
        //ChangeCameraView();
        canSwitchCamera = true;
        break;
    }
  }

  void OnTriggerExit(Collider otherC) {

    switch (otherC.gameObject.tag) {
      case "PerspectiveChangeZone":
        //CameraAnimator.SetBool("IsCamera3D",false);
        canSwitchCamera = false;
        cameraController.SetMode(use2DCamera: true);
        break;
    }
  }

  void ChangeCameraView() {
    if (!canSwitchCamera) {
      return;
    }

    if (Input.GetKeyDown(KeyCode.F) && cameraController.IsCamera3D == false) {
      cameraController.SetMode(use2DCamera: false);


    } else if (Input.GetKeyDown(KeyCode.F) && cameraController.IsCamera3D == true) {
      cameraController.SetMode(use2DCamera: true);

    }

  }


}
