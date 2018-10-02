using UnityEngine;

public class TemplateUnit : MonoBehaviour
{
    /// <summary>
    /// Information about the unit.
    /// </summary>
    public UnitInfo unit;

    // Text above the unit offsets
    private const float _countOffset = 0.7f;
    private const float _nonCountOffset = 0f;

    // Use this for initialization
    void Start()
    {
        // Assign the unit's model to the mesh filter
        gameObject.GetComponent<MeshFilter>().mesh = unit.model;

        // Get the height of the model
        var modelHeight = /*gameObject.transform.position.y + */unit.model.bounds.size.y;

        // Adjust the UI according to the model's height
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Get the child by index
            var child = this.transform.GetChild(i);

            // Get an alias to the position
            var pos = child.transform.position;

            // Define the child's y position based on the model's height
            var y = modelHeight;

            // Increase the y based on the offset
            if (child.name == "Count")
                y += _countOffset;
            else
                y += _nonCountOffset;

            // Adjust the child's position
            child.transform.position = new Vector3(pos.x, y, pos.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust the rotation of the UI
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Find the child of the object
            var child = this.transform.GetChild(i);

            // Adjust the child's rotation based on the main camera's rotation
            child.transform.rotation = Camera.main.transform.rotation;
        }
    }
}
