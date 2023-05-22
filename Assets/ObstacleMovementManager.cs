using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovementManager : MonoBehaviour
{
    public Vector3 LeftPos;
    public Vector3 RightPos;
    public Vector3 UpPos;
    public Vector3 DownPos;
    Vector3 CurrentPos; 
    Material ObjectMaterial;  
    float desiredDuration = 100f;
    float timeElapsed; 
    public CameraController cameraController;  
    public ObstacleSelectionManager obstacleSelectionManager;      

    // Start is called before the first frame update
    void Start()
    {
        CurrentPos = transform.position;
        ObjectMaterial = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMovement(); 
        
    }

   void ObstacleMovement()
   {

    if(Input.GetKey(KeyCode.R) && cameraController.IsCamera3D == true && obstacleSelectionManager.isSelected == true)
    {
        Debug.Log("Move");
        timeElapsed += Time.deltaTime; 
        CurrentPos = Vector3.Lerp(CurrentPos,LeftPos,timeElapsed/desiredDuration);
        transform.position = CurrentPos; 
    }

 
   }
}
