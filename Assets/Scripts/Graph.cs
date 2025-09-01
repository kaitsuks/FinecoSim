using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Draws a simple graph using LineRenderer
/// and shows title + legend text with TextMeshPro (set directly in the scene).
/// Supports adding values either with (x,y) or just y.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Graph Settings")]
    public int pointCount = 50;     // max number of points to keep
    public float graphWidth = 10f;  // x-axis width
    public float graphHeight = 5f;  // y-axis scaling

    [Header("Text Labels (drag in from scene)")]
    public TextMeshProUGUI titleText;   // text object for the graph title
    public TextMeshProUGUI legendText;  // text object for the legend/explanation

    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private int autoX = 0; // automatic x counter if only y-values are added

    void Start()
    {
        // Get LineRenderer from the same GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Optional default texts
        if (titleText != null)
            titleText.text = "Government Net Budget";

        if (legendText != null)
            legendText.text = "y over time";
    }

    /// <summary>
    /// Add a new point with explicit x and y.
    /// </summary>
    public void AddValue(float x, float y)
    {
        if (points.Count >= pointCount)
            points.RemoveAt(0);

        points.Add(new Vector3(x, y, 0));
        UpdateGraph();
    }

    /// <summary>
    /// Add a new point with only y.
    /// X is automatically increased by 1 for each call.
    /// </summary>
    public void AddValue(float y)
    {
        AddValue(autoX, y);
        autoX++;
    }

    /// <summary>
    /// Redraws the graph in the LineRenderer.
    /// </summary>
    private void UpdateGraph()
    {
        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            // Normalize x and y to fit graphWidth and graphHeight
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = Mathf.Clamp(points[i].y, -graphHeight, graphHeight);

            lineRenderer.SetPosition(i, new Vector3(normX, normY, 0));
        }
    }
}

