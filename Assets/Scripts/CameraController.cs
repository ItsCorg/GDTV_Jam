using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour {

  public CinemachineVirtualCamera Camera3D;
  public CinemachineVirtualCamera Camera2D;

  [SerializeField]
  bool is2DMode = true; // set to the same perspective that the game is in at the start

  void Start() {
    SetMode(is2DMode);
  }

  public void SetMode(bool use2DCamera) {
    is2DMode = use2DCamera;
    Camera3D.Priority = is2DMode ? 0 : 1;
    Camera2D.Priority = is2DMode ? 1 : 0;
    Debug.Log("camera is 2d: " + is2DMode);
  }

  public bool IsCamera3D => !is2DMode;
}
