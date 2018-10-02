using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Should the camera movement be allowed? (Using the WASD, arrow keys, etc. or by
    /// going near the edge of screen)
    /// </summary>
    [Header("Allowments")]
    public bool allowMovement;

    /// <summary>
    /// Should the camera zoom be allowed?
    /// </summary>
    public bool allowZoom;

    /// <summary>
    /// Should the camera rotation be allowed?
    /// </summary>
    public bool allowRotation;

    /// <summary>
    /// Multiplier for the offset near the edge of the screen.
    /// (How close to the edge should the mouse go to trigger
    /// camera movement)
    /// </summary>
    [Header("Movement multipliers")]
    public float edgeOfScreenOffsetMultiplier;

    /// <summary>
    /// Multiplier for zoom speed.
    /// </summary>
    public float zoomMultiplier;
    /// <summary>
    /// Multiplier for the rotation speed.
    /// </summary>
    public float rotationMultiplier;
    /// <summary>
    /// Multiplier for the movement speed.
    /// </summary>
    public float movementSpeedMultiplier;

    // Offset from the edge of the screen 
    // for the camera movement
    private float _edgeOfScreenOffset;
    // Rotation around the y axis
    private float _yRotation = 1f;

    // Use this for initialization
    void Start()
    {
        // Look at the camera rig
        Camera.main.transform.LookAt(this.transform);

        // Calculate the offset based on the screen width
        _edgeOfScreenOffset = Screen.width * edgeOfScreenOffsetMultiplier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (allowMovement)
        {
            // Move based on WASD, arrows keys etc...
            MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Right edge of the screen
            if (Input.mousePosition.x > Screen.width - _edgeOfScreenOffset)
                MoveCamera(1, 0);

            // Left edge of the screen
            if (Input.mousePosition.x < _edgeOfScreenOffset)
                MoveCamera(-1, 0);

            // Top edge of the screen
            if (Input.mousePosition.y > Screen.height - _edgeOfScreenOffset)
                MoveCamera(0, 1);

            // Bottom edge of the screen
            if (Input.mousePosition.y < _edgeOfScreenOffset)
                MoveCamera(0, -1);
        }

        if (allowRotation)
            RotateCamera();

        if (allowZoom)
            ZoomCamera();
    }

    // WASD or edge of screen
    void MoveCamera(float horizontalAxis, float verticalAxis)
    {
        // Create the vectors based on the rotation around the y axis
        var forward = new Vector3(0, 0, Mathf.Abs(_yRotation));
        var right = new Vector3(Mathf.Abs(_yRotation), 0, 0);

        // Normalize the movement bro
        forward.Normalize();
        right.Normalize();

        // Get the direction in the world space we want to move to
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        // Apply the movement
        transform.Translate(desiredMoveDirection * movementSpeedMultiplier * Time.deltaTime);
    }

    // While holding the right mouse button
    void RotateCamera()
    {
        // If the RMB is being held down...
        if (Input.GetKey(KeyCode.Mouse1))
        {
            // Increase the y rotation based on the x axis delta movement
            _yRotation += Input.GetAxis("Mouse X") * rotationMultiplier * Time.deltaTime;

            // Adjust the camera angle
            this.transform.localEulerAngles = new Vector3(0, _yRotation, 0);
        }
    }

    // Scrolling
    void ZoomCamera()
    {
        // If there is scrolling action...
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            // Adjust the field of view of the camera based on the scrolling
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier;
    }
}
