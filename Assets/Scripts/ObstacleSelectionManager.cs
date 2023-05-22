using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSelectionManager : MonoBehaviour {

  [SerializeField]
  Material selectableMaterial;
  [SerializeField]
  Material selectedMaterial;
  [SerializeField]
  Material defaultMaterial;

  Transform _hoveredObstacle;
  Transform _selectedObstacle;

  public bool isSelected; 

  void Update() {

    // if we were hovering over an obstacle and are not hovering over it anymore, then we reset its material (unless the obstacle is selected for moving)
    if (_hoveredObstacle != null && _hoveredObstacle != _selectedObstacle) {
      _hoveredObstacle.GetComponent<Renderer>().material = defaultMaterial;
      _hoveredObstacle = null;
    }
    // deselect the selected obstacle on right-mouse button
    if (Input.GetMouseButtonDown(1) && _selectedObstacle != null) {
      _selectedObstacle.GetComponent<Renderer>().material = defaultMaterial;
      _selectedObstacle = null;
    }

    // Raycast against ObstacleLayer to check if player is hovering over obstacle with their mouse:
    int layerMask = 1 << LayerMask.NameToLayer("ObstacleLayer");
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, maxDistance: Mathf.Infinity, layerMask: layerMask)) {
      var obstacle = hit.transform;
      var obstacleRenderer = obstacle.GetComponent<Renderer>();

      var mousePressed = Input.GetMouseButtonDown(0);

      // if we're hovering over the selected obstacle then no need to do anything
      if (obstacle == _selectedObstacle) {
        return;
      }

      // set the material of the obstacle we're hovering over, depending on if the player just clicked the mouse button or is just hovering
      obstacleRenderer.material = mousePressed ? selectedMaterial : selectableMaterial;
      if (mousePressed) {
         
        // if we already had an obstacle selected, deselect it now
        if (_selectedObstacle) {
          _selectedObstacle.GetComponent<Renderer>().material = defaultMaterial;
          isSelected = false;
        }

        // store the obstacle we just selected
        _selectedObstacle = obstacle;
        isSelected = true; 
        //StartObstacleMovement();
      }
      _hoveredObstacle = obstacle;
    }
  }
  
  /*
  void StartObstacleMovement() {
    _selectedObstacle.GetComponent<ObstacleDrag>().StartDrag();
    return;

    for (var idx = 0; idx < _selectedObstacle.transform.childCount; idx++) {
      var child = _selectedObstacle.transform.GetChild(idx);

      child.GetComponent<ObstacleDrag>().StartDrag();
    }
  }
  */
}
