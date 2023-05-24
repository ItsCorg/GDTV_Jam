using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

  public CinemachineVirtualCamera Camera3D;
  public CinemachineVirtualCamera Camera2D;
  public CharacterMovement characterMovement;
  public ObstacleSelectionManager obstacleSelectionManager;

  [SerializeField]
  bool is2DMode = true; // set to the same perspective that the game is in at the start
  bool isTongueOut = false;

  void Start() {
    SetMode(is2DMode);
  }

  public void SetMode(bool use2DCamera) {
    bool didSwitchCamera = is2DMode != use2DCamera;

    is2DMode = use2DCamera;
    Camera3D.Priority = is2DMode ? 0 : 1;
    Camera2D.Priority = is2DMode ? 1 : 0;

    if (didSwitchCamera) {
      StartCoroutine(PlayAnimationOnce());
    }
    
    //Debug.Log("camera is 2d: " + is2DMode);
    if (is2DMode) {
      // when switching back to 2d, we need to deselect and obstacle if it was selected
      obstacleSelectionManager.DeselectObstacle();
      
      
    }
    Camera.main.orthographic = is2DMode;
  }

  public bool IsCamera3D => !is2DMode;
  private IEnumerator PlayAnimationOnce() {
    characterMovement.myAnimator.SetBool("IsCamera3D", true);
    yield return null;
    characterMovement.myAnimator.SetBool("IsCamera3D", false);
  }
}
