using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public bool canMove = false;
    public float upDistance = 1f;
    public float downDistance = 1f;
    public float leftDistance = 1f;
    public float rightDistance = 1f;
    public float axisSwapRange = 0.1f;
    [SerializeField]private float moveSpeed;

    private Vector3 originalPosition;
    public Material lineMaterial;
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
        Vector3 movement = Vector3.zero;

        // Check arrow key inputs
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += Vector3.left;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement += Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movement += Vector3.down;
        }

        movement *= Time.deltaTime * moveSpeed;

        // Apply distance limits
        Vector3 newPosition = transform.position + movement;
        newPosition.x = Mathf.Clamp(newPosition.x, originalPosition.x - leftDistance, originalPosition.x + rightDistance);
        newPosition.y = Mathf.Clamp(newPosition.y, originalPosition.y - downDistance, originalPosition.y + upDistance);

        transform.position = newPosition;

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
