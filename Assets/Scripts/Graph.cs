using UnityEngine;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Draws a graph centered at (0,0) using LineRenderer.
/// - Graph is normalized to given width/height.
/// - Supports adding values with (x,y) or just y.
/// - Optionally shows X and Y axes.
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Graph Settings")]
    public int pointCount = 50;     // Max number of points to keep
    public float graphWidth = 10f;  // Total width (spans -width/2 to +width/2)
    public float graphHeight = 5f;  // Total height scaling (spans -height/2 to +height/2)

    [Header("Text Labels (drag in from scene)")]
    public TextMeshProUGUI titleText;   // Title text object
    public TextMeshProUGUI legendText;  // Legend text object

    [Header("Line Style")]
    //colour of graph
    public Color lineColor = Color.green;
    //thickness of line
    public float lineThickness = 5f;

    [Header("Axis Settings")]
    public bool showAxes = true;     // Toggle drawing axes
    public Color axisColor = Color.white;
    public float axisThickness = 0.02f;

    private LineRenderer lineRenderer;         // Main line renderer for the graph
    private LineRenderer axisRenderer;         // Separate line renderer for axes
    private List<Vector3> points = new List<Vector3>();
    private int autoX = 0; // Automatic x counter if only y-values are added

    void Start()
    {
        // Main graph line
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.widthMultiplier = lineThickness;

        // Create second LineRenderer for axes if needed
        if (showAxes)
        {
            GameObject axisObj = new GameObject("GraphAxes");
            axisObj.transform.SetParent(transform, false);
            axisRenderer = axisObj.AddComponent<LineRenderer>();
            axisRenderer.material = new Material(Shader.Find("Sprites/Default"));
            axisRenderer.startColor = axisColor;
            axisRenderer.endColor = axisColor;
            axisRenderer.widthMultiplier = axisThickness;
            axisRenderer.positionCount = 4; // X-axis (2 points) + Y-axis (2 points)
        }

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
    /// Redraws the graph and optional axes.
    /// </summary>
    private void UpdateGraph()
    {
        if (points.Count == 0) return;

        lineRenderer.positionCount = points.Count;

        // Determine min/max X and Y in the data
        float minX = float.MaxValue, maxX = float.MinValue;
        float minY = float.MaxValue, maxY = float.MinValue;

        foreach (var p in points)
        {
            if (p.x < minX) minX = p.x;
            if (p.x > maxX) maxX = p.x;
            if (p.y < minY) minY = p.y;
            if (p.y > maxY) maxY = p.y;
        }

        float rangeX = Mathf.Max(1e-5f, maxX - minX);
        float rangeY = Mathf.Max(1e-5f, maxY - minY);

        for (int i = 0; i < points.Count; i++)
        {
            // Normalize each point into [-0.5, +0.5] range
            float normX = ((points[i].x - minX) / rangeX - 0.5f) * graphWidth;
            float normY = ((points[i].y - minY) / rangeY - 0.5f) * graphHeight;

            lineRenderer.SetPosition(i, new Vector3(normX, normY, 0));
        }

        // Draw axes if enabled
        if (showAxes && axisRenderer != null)
        {
            axisRenderer.positionCount = 4;
            axisRenderer.SetPosition(0, new Vector3(-graphWidth / 2f, 0, 0));
            axisRenderer.SetPosition(1, new Vector3(graphWidth / 2f, 0, 0));
            axisRenderer.SetPosition(2, new Vector3(0, -graphHeight / 2f, 0));
            axisRenderer.SetPosition(3, new Vector3(0, graphHeight / 2f, 0));
        }
    }

}
