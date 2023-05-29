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
  [SerializeField] private ParticleSystem stepParticle;
  [SerializeField] private ParticleSystem jumpParticle;
  public AudioClip[] walkSounds; // Array of walk sound variations
  public AudioClip JumpSound; //jump sound
  public AudioSource soundEffect; // Reference to the AudioSource component
  
  

    [SerializeField] private GameObject tongue;

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
  public GameObject EndScenario; 
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

    ChangeCameraView();
  }

  void FixedUpdate() {
    if (isDead) { return; }

    Movement();
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
      //particleS.Stop();
      myAnimator.SetFloat("Speed", Mathf.Abs(xValue));

        if (Mathf.Abs(xValue) > 0) {
        if (!soundEffect.isPlaying) {
            AudioClip walkSound = walkSounds[Random.Range(0, walkSounds.Length)];
            soundEffect.clip = walkSound;
            soundEffect.Play();
        }
    } else {
        soundEffect.Stop();
    }

      //Jumping
      if (jumpPressed) {
        soundEffect.clip = JumpSound;
        soundEffect.Play();  
        jumpPressed = false;
        isGrounded = false;
        myAnimator.SetBool("isJumping", true);
        myRigidbody.AddForce(Vector3.up * JumpForce);
        JumpCount--;
      } else {
        isGrounded = true;
      }

      /*
      //Flipping sprite
      if (xValue < 0) {
        // flipX in spriteRenderer: Only the rendering is affected. Use negative Transform.scale, if you want to affect all the other components (for example colliders).
        //spriteRenderer.flipX = false;
      } else if (xValue > 0) {
        //spriteRenderer.flipX = true;
      }
      */

      //handle flip and particle direction
      if (xValue > 0) {
        spriteRenderer.flipX = true;

            stepParticle.transform.position = transform.position + new Vector3(-0.5f, -0.5f);
            stepParticle.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            jumpParticle.transform.position = transform.position + new Vector3(-0.5f, -0.5f);
            jumpParticle.transform.rotation = Quaternion.Euler(45f, -90f, 0f);
            tongue.transform.position = transform.position + new Vector3(0.35f, -0.3f);
        }
        else if (xValue < 0) {
            spriteRenderer.flipX = false;

            stepParticle.transform.position = transform.position + new Vector3(0.5f, -0.5f);
            stepParticle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            jumpParticle.transform.position = transform.position + new Vector3(0.5f, -0.5f);
            jumpParticle.transform.rotation = Quaternion.Euler(45f, 90f, 0f);
            tongue.transform.position = transform.position + new Vector3(-0.35f, -0.3f);
        }

        
    }

    //Called every step
    public void FrogStep() {
        stepParticle.Play();
    }

    //Called every jump
    public void FrogJump() {
        jumpParticle.Play();
    }

    

//commented this out because in the usual case you dont wanna flip the whole object as that can cause issues later
//its better to just use spriterenderer.flip
/*
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
*/

  void OnCollisionEnter(Collision other) {
    //Reset Jump Count when touching a platform
    switch (other.gameObject.tag) {
            case "Obstacle":
       JumpCount = 1;
       isGrounded = true;
       myAnimator.SetBool("isJumping", false);
       break;
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
        //Win();
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

 public void Win() {
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


