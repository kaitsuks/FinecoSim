// Bring in Unity's core library so we can use MonoBehaviour, LineRenderer, Mathf, etc.
using UnityEngine;

// Define a new component class called GraphPlotter that can be attached to a GameObject in Unity
public class GraphPlotter : MonoBehaviour
{
    // Reference to a LineRenderer component (will be set in the Inspector or added to the same GameObject)
    public LineRenderer lineRenderer;

    // Number of points (vertices) to plot on the graph
    public int pointCount = 50;

    // The total width of the graph along the X-axis
    public float graphWidth = 10f;

    // The vertical scaling factor for the graph (multiplies the y-values)
    public float graphHeight = 5f;

    // Unity automatically calls Start() once at the beginning when the script is first run
    void Start()
    {
        // Tell the LineRenderer how many points (positions) it will draw
        lineRenderer.positionCount = pointCount;

        // Loop through each point index from 0 up to (pointCount - 1)
        for (int i = 0; i < pointCount; i++)
        {
            // Calculate the normalized X position: from 0 → graphWidth
            // i / (pointCount - 1) gives a fraction (0.0 to 1.0), then multiply by graphWidth
            float x = (i / (float)(pointCount - 1)) * graphWidth;

            // Calculate the Y position using the sine function, scaled by graphHeight
            // This creates the up-and-down wave shape
            float y = Mathf.Sin(x) * graphHeight; // example function

            // Set the i-th point of the LineRenderer at (x, y, z=0)
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
