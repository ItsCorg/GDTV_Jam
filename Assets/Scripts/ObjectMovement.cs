using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    public bool canMove = false; // temp bool for testing
    public float upDistance = 1f;
    public float downDistance = 1f;
    public float leftDistance = 1f;
    public float rightDistance = 1f;
    public float axisSwapRange = 0.1f;

    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = prefab.transform.position;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveObject();
        }
    }

    private void MoveObject()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        //handle movement depending on mouse position
        Vector3 mousePositionScreen = new Vector3(mouseX, mouseY, Camera.main.transform.position.y);
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector3 movement = mousePositionWorld - originalPosition;
        movement.z = 0f;   // lock on Z axis

        //handle axis swtich
        bool outsideRange = Mathf.Abs(movement.x) > axisSwapRange || Mathf.Abs(movement.y) > axisSwapRange;

        if (outsideRange)
        {
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                movement.y = 0f;
            }
            else
            {
                movement.x = 0f;
            }
        }

        //limit how far object can go
        movement.x = Mathf.Clamp(movement.x, -leftDistance, rightDistance);
        movement.y = Mathf.Clamp(movement.y, -downDistance, upDistance);

        prefab.transform.position = originalPosition + movement;
    }
}