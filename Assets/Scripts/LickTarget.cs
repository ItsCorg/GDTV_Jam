using System.Collections;
using UnityEngine;

public class LickTarget : MonoBehaviour {

  [SerializeField]
  Animator animator;

  [SerializeField]
  Transform targetPosition;

  public Transform TargetPosition => targetPosition;

  bool inRange = false;

  void Start() {
    animator = GetComponent<Animator>();
  }

  public void ToggleInRange(bool inRange) {
    this.inRange = inRange;
    animator.SetBool("inRange", inRange);
  }

}