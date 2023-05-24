using System.Collections;
using UnityEngine;

public class LickTarget : MonoBehaviour {

  [SerializeField]
  Animator animator;

  bool inRange = false;

  void Start() {
    animator = GetComponent<Animator>();
  }

  public void ToggleInRange(bool inRange) {
    Debug.Log("lick target " + inRange);
    this.inRange = inRange;
    animator.SetBool("inRange", inRange);
  }

}