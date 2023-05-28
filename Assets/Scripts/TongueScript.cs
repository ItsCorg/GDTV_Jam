using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueScript : MonoBehaviour {

  public CharacterMovement characterMovement;
  void Start() {
    lr = GetComponent<LineRenderer>();
  }

  void Update() {
    RollTongue();

    if (isPlayingTongueAnim) {
      AnimateTongue();
    }
  }

  public Transform tongueOutPosition;
  public Transform tongueTarget;
  LineRenderer lr;

  float elapsedTime;
  public float tongueAnimTime = .5f;
  bool isPlayingTongueAnim;
  bool returningTongue;

  void PlayTongueLineRenderer() {
    lr.positionCount = 2;
    lr.SetPosition(0, tongueOutPosition.position);
    lr.SetPosition(1, tongueOutPosition.position);
    elapsedTime = 0f;
    isPlayingTongueAnim = true;
    returningTongue = false;

  }
  void AnimateTongue() {
    elapsedTime = returningTongue ? elapsedTime - Time.deltaTime : elapsedTime + Time.deltaTime;

    var percentage = elapsedTime / tongueAnimTime;
    var currPos = Vector3.Lerp(tongueOutPosition.position, tongueTarget.position, percentage);
    lr.SetPosition(0, tongueOutPosition.position);
    lr.SetPosition(1, currPos);
    if (percentage >= 1f && !returningTongue) {
      returningTongue = true;
    }

    if (percentage <= 0 && returningTongue) {
      lr.positionCount = 0;
      isPlayingTongueAnim = false;
      returningTongue = false;
    }
  }

  void RollTongue() {
    if (!characterMovement.canSwitchCamera) {
      return;
    }

    if (characterMovement.Is3DMode() && !isPlayingTongueAnim) {
      PlayTongueLineRenderer();
    }
  }
}
