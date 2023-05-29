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
  public AudioSource soundEffect; // Reference to the AudioSource component for sound effects
  public AudioClip transitionTo3DSound; // The audio clip for the transition to 3D sound effect
  public AudioClip transitionTo2DSound; // The audio clip for the transition to 2D sound effect

  void Start() {
    SetMode(is2DMode);
  }

  public void SetMode(bool use2DCamera) {
    bool didSwitchCamera = is2DMode != use2DCamera;

    is2DMode = use2DCamera;
    Camera3D.Priority = is2DMode ? 0 : 1;
    Camera2D.Priority = is2DMode ? 1 : 0;

    if (didSwitchCamera) {
      characterMovement.PlayTongueAnimation();
      // Play the corresponding transition sound effect
      if (is2DMode)
      {
        soundEffect.clip = transitionTo2DSound;
      }
      else
      {
        soundEffect.clip = transitionTo3DSound;
      }
      soundEffect.Play();
    }
    
    //Debug.Log("camera is 2d: " + is2DMode);
    if (is2DMode) {
      // when switching back to 2d, we need to deselect obstacle if it was selected
      obstacleSelectionManager.DeselectObstacle();
    }
    Camera.main.orthographic = is2DMode;
  }

  public bool IsCamera3D => !is2DMode;

}
