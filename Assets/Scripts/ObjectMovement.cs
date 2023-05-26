using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public bool canMove = false;
    public float upDistance = 1f;
    public float downDistance = 1f;
    public float leftDistance = 1f;
    public float rightDistance = 1f;
    public float axisSwapRange = 0.1f;
    public Material lineMaterial;

    private Vector3 originalPosition;
    private LineRenderer upLineRenderer;
    private LineRenderer downLineRenderer;
    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;

    private void Awake()
    {
        originalPosition = transform.position;

        // Create and configure line renderers for each direction
        upLineRenderer = CreateLineRenderer();
        downLineRenderer = CreateLineRenderer();
        leftLineRenderer = CreateLineRenderer();
        rightLineRenderer = CreateLineRenderer();

        // Set initial positions for the line renderers
        UpdateLineRendererPositions();
    }

    private void Update()
    {
        if (canMove)
        {
            MoveObject();

            upLineRenderer.enabled = true;
            downLineRenderer.enabled = true;
            rightLineRenderer.enabled = true;
            leftLineRenderer.enabled = true;
        }
        else
        {
            upLineRenderer.enabled = false;
            downLineRenderer.enabled = false;
            rightLineRenderer.enabled = false;
            leftLineRenderer.enabled = false;
        }

    }

    private void MoveObject()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        Vector3 mousePositionScreen = new Vector3(mouseX, mouseY, Camera.main.transform.position.y);
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector3 movement = mousePositionWorld - originalPosition;
        movement.z = 0f;

        // Check if outside axis swap range
        bool outsideRange = Mathf.Abs(movement.x) > axisSwapRange || Mathf.Abs(movement.y) > axisSwapRange;

        if (outsideRange)
        {
            // Only allow movement on the dominant axis
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                movement.y = 0f;
            }
            else
            {
                movement.x = 0f;
            }
        }

        // Apply distance limits
        movement.x = Mathf.Clamp(movement.x, -leftDistance, rightDistance);
        movement.y = Mathf.Clamp(movement.y, -downDistance, upDistance);

        transform.position = originalPosition + movement;

        UpdateLineRendererPositions();
    }

    private LineRenderer CreateLineRenderer()
    {
        GameObject lineObject = new GameObject("LineRenderer");
        lineObject.transform.SetParent(transform);
        lineObject.transform.localPosition = Vector3.zero;

        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 1f;
        lineRenderer.endWidth = 1;

        lineRenderer.material = lineMaterial; // Assign the desired material from the inspector
        lineRenderer.sortingLayerName = "LineRenderer"; // Set the sorting layer to "LineRenderer"
        lineRenderer.textureMode = LineTextureMode.Tile; // Set the texture mode to "Tile"

        return lineRenderer;
    }

    private void UpdateLineRendererPositions()
    {
        upLineRenderer.enabled = canMove;
        downLineRenderer.enabled = canMove;
        leftLineRenderer.enabled = canMove;
        rightLineRenderer.enabled = canMove;


        upLineRenderer.SetPosition(0, originalPosition);
        upLineRenderer.SetPosition(1, originalPosition + Vector3.up * upDistance);

        downLineRenderer.SetPosition(0, originalPosition);
        downLineRenderer.SetPosition(1, originalPosition + Vector3.down * downDistance);

        leftLineRenderer.SetPosition(0, originalPosition);
        leftLineRenderer.SetPosition(1, originalPosition + Vector3.left * leftDistance);

        rightLineRenderer.SetPosition(0, originalPosition);
        rightLineRenderer.SetPosition(1, originalPosition + Vector3.right * rightDistance);
    }
}
