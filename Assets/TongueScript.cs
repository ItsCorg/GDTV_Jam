using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class TongueScript : MonoBehaviour {
  public CharacterMovement characterMovement;
  Animator TongueAnimator;
  float StrechValue;

  public GameObject Frog;
  public GameObject Obstacle;
  Vector3 FrogToObstaclePos;
  // Start is called before the first frame update
  void Start() {
    TongueAnimator = GetComponent<Animator>();
    FrogToObstaclePos = new Vector3(Frog.transform.position.x / Obstacle.transform.position.x, Frog.transform.position.y / Obstacle.transform.position.y, Frog.transform.position.z / Obstacle.transform.position.z);

    lr = GetComponent<LineRenderer>();
  }

  // Update is called once per frame
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

    if (characterMovement.myAnimator.GetBool("IsCamera3D") == true && TongueAnimator.GetBool("RollTongueOut") == false) {

      //TongueAnimator.SetBool("RollTongueOut", true);
      PlayTongueLineRenderer();

    } else if (characterMovement.myAnimator.GetBool("IsCamera3D") == false && TongueAnimator.GetBool("RollTongueOut") == true) {

      //TongueAnimator.SetBool("RollTongueOut", false);
    }
  }
}
