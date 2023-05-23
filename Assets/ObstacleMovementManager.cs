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
    public Vector3 InitialPos; 
    Material ObjectMaterial;  
    float desiredDuration = 100f;
    float timeElapsed; 
    public CameraController cameraController;  
    public ObstacleSelectionManager obstacleSelectionManager;      

    // Start is called before the first frame update
    void Start()
    {
        CurrentPos = transform.position;
        //InitialPos = new Vector3(3.09f,1.75f,0.06f);
        ObjectMaterial = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
        ObstacleMovement(); 
        
    }

   void ObstacleMovement()
   {
   // bool isLeft = false;

    if(Input.GetKey(KeyCode.R) && cameraController.IsCamera3D == true && obstacleSelectionManager.isSelected == true   )
    {
        Debug.Log("Move");
        //isLeft = true; 
        timeElapsed += Time.deltaTime; 
        CurrentPos = Vector3.Lerp(CurrentPos,LeftPos,timeElapsed/desiredDuration);
        transform.position = CurrentPos;
         
    } else if(Input.GetKey(KeyCode.T) && cameraController.IsCamera3D == true && obstacleSelectionManager.isSelected == true ){
        //isLeft = false;
        //Vector3 InitialPos = new Vector3 (3.09f,1.75f,0.06f);
        CurrentPos = Vector3.Lerp(LeftPos,InitialPos,timeElapsed/desiredDuration);
        transform.position = CurrentPos;
    } 

 
   }
}
