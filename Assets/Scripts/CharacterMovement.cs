using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.SceneManagement; 

public class CharacterMovement : MonoBehaviour {

  [SerializeField] float MovementSpeed;
  [SerializeField] float JumpForce;
  [SerializeField] int JumpCount;
  [SerializeField] bool isGrounded;
  Rigidbody myRigidbody;

  float xValue;
  SpriteRenderer spriteRenderer;
  [SerializeField]
  Animator myAnimator;
  bool isDead = false;

  public GameObject Canva;
  public GameObject CameraController; 
  
  //public Animator CameraAnimator;
  public CameraController cameraController;
  public MenuController menuController;
  public Transform CheckPointTransform;

  [SerializeField]
  GameObject WinGameUI;

  [SerializeField]
  TongueScript tongueScript;


  // Start is called before the first frame update
  void Start() {
    myRigidbody = GetComponent<Rigidbody>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    myAnimator = GetComponent<Animator>();
    isGrounded = true;
    xValue = 0;
    Canva.SetActive(false);
    CheckPointTransform.transform.position = transform.position; // initialize checkpoint to Frog position at start of game
    WinGameUI.SetActive(false);
  }

  bool jumpPressed;
  void Update() {
    if (isDead) { return; }

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && JumpCount == 1) {
      jumpPressed = true;
    }
  }

  void FixedUpdate() {
    if (isDead) { return; }

    Movement();
    ChangeCameraView();
  }

  void Movement() {

    if (cameraController.IsCamera3D) {
       myAnimator.SetFloat("Speed", 0);
       return;
    }

      //Movement
      isGrounded = true;
      xValue = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
      transform.Translate(xValue, 0, 0);
      myAnimator.SetFloat("Speed", Mathf.Abs(xValue));

      //Jumping
      if (jumpPressed) {
        jumpPressed = false;
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
        canSwitchCamera = true;
        Cother.transform.parent.GetComponent<LickTarget>()?.ToggleInRange(true);
        tongueScript.tongueTarget = Cother.gameObject.transform.parent.GetComponent<LickTarget>().TargetPosition;
        break;

        case "DeathZone":
        Die();
        CameraController.SetActive(false);
        break;
        case "WinZone":
        Win();
        CameraController.SetActive(false);
        break;

      case "CheckPoints":
        // TODO improvement would be that if player goes back in the level they don't activate previous checkpoints but instead keep the furthest checkpoint active
        CheckPointTransform.position = Cother.transform.position;
        break;
  }
  }
  void OnTriggerExit(Collider otherC) {

    switch (otherC.gameObject.tag) {
      case "PerspectiveChangeZone":
        canSwitchCamera = false;
        cameraController.SetMode(use2DCamera: true);
        otherC.transform.parent.GetComponent<LickTarget>()?.ToggleInRange(false);
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

  public void PlayTongueAnimation() {
    StartCoroutine(PlayAnimationOnce());
  }
  private IEnumerator PlayAnimationOnce() {
    myAnimator.SetBool("IsCamera3D", true);
    yield return null;
    myAnimator.SetBool("IsCamera3D", false);
  }

  public bool Is3DMode() {
    return myAnimator.GetBool("IsCamera3D");
  }

  void Win() {
    WinGameUI.SetActive(true);
    Die();
  }

  void Die() {
    myAnimator.enabled = false;
    isDead = true;
    Canva.SetActive(true);
  }

  public void RetryLastCheckpoint()
    {
        myAnimator.enabled = true;
        transform.position = CheckPointTransform.position;
        isDead = false;
        Canva.SetActive(false);
        CameraController.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        CameraController.SetActive(true);
    }

    public void GoToMainMenu()
    {
        menuController.MainMenu();
        CameraController.SetActive(true);
    }


}


