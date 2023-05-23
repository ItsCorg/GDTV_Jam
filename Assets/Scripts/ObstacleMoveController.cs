using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ObstacleMoveController : MonoBehaviour {

  public enum ObstaclePosition {
    Up, Down, Left, Right, Initial
  }

  ObstaclePosition currPos = ObstaclePosition.Initial;

  public Transform UpPos;
  public Transform DownPos;
  public Transform LeftPos;
  public Transform RightPos;

  public float timeForObstacleMove = .33f;
  float timeMoved;

  Vector3 initialPos;
  Vector3 startPos;
  Vector3 goalPos;

  bool isMoving = false;

  bool isSelected = false;
  public void ToggleSelection() => isSelected = !isSelected;

  void Start() {
    initialPos = transform.position;
  }

  void Update() {
    if (isSelected) {
      CheckInput();
    }
    if (isMoving) {
      MoveObstacle();
    }
  }

  void CheckInput() {
    if (isMoving) {
      return;
    }

    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) {

      startPos = transform.position;

      if (currPos == ObstaclePosition.Initial) {
        ObstaclePosition dir = GetPositionForKeyInput();
        var maybeGoalPos = GetWorldPosition(dir);
        if (maybeGoalPos.HasValue) {
          goalPos = maybeGoalPos.Value;
          currPos = dir;

          isMoving = true;
          timeMoved = 0f;
        }

      } else if (OppositeKeyForCurrentPositionPressed()) {
        goalPos = GetWorldPosition(ObstaclePosition.Initial).Value;
        currPos = ObstaclePosition.Initial;

        isMoving = true;
        timeMoved = 0f;
      } else {

      }
    }
  }
  ObstaclePosition GetPositionForKeyInput() {
    if (Input.GetKeyDown(KeyCode.UpArrow)) {
      return ObstaclePosition.Up;
    }
    if (Input.GetKeyDown(KeyCode.DownArrow)) {
      return ObstaclePosition.Down;
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow)) {
      return ObstaclePosition.Left;
    }
    if (Input.GetKeyDown(KeyCode.RightArrow)) {
      return ObstaclePosition.Right;
    }
    throw new System.Exception();
  }
  Vector3? GetWorldPosition(ObstaclePosition pos) {
    Transform posTransform = null;
    switch (pos) {
      case ObstaclePosition.Up: posTransform = UpPos; break;
      case ObstaclePosition.Down: posTransform = DownPos; break;
      case ObstaclePosition.Left: posTransform = LeftPos; break;
      case ObstaclePosition.Right: posTransform = RightPos; break;
      case ObstaclePosition.Initial: return initialPos;
    }
    if (posTransform != null) {
      return posTransform.position;
    }
    return null;
  }
  bool OppositeKeyForCurrentPositionPressed() {
    switch (currPos) {
      case ObstaclePosition.Up: return Input.GetKeyDown(KeyCode.DownArrow);
      case ObstaclePosition.Down: return Input.GetKeyDown(KeyCode.UpArrow);
      case ObstaclePosition.Left: return Input.GetKeyDown(KeyCode.RightArrow);
      case ObstaclePosition.Right: return Input.GetKeyDown(KeyCode.LeftArrow);
      case ObstaclePosition.Initial: return false;
    }
    return false;
  }

  void MoveObstacle() {
    timeMoved += Time.deltaTime;
    var percentage = timeMoved / timeForObstacleMove;
    var currPos = Vector3.Lerp(startPos, goalPos, percentage);
    transform.position = currPos;

    if (percentage >= 1f) {
      //Debug.Log("end position reached");
      isMoving = false;
      return;
    }
  }
}