using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Text Labels")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI legendText;

    [Header("Graph Settings")]
    public int pointCount = 50;        // number of points visible at once
    public float graphWidth = 10f;     // width of the graph in local units
    public float graphHeight = 5f;     // height of the graph in local units
    public float yOffset = 0f;         // vertical offset (optional)

    private LineRenderer lineRenderer;
    private List<float> values = new List<float>();
    private int totalPointsAdded = 0;

    void Start()
    {
        if (titleText != null)
            titleText.text = "Government Net Budget";

        if (legendText != null)
            legendText.text = "y over time";

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.widthMultiplier = 0.15f;

        // Use local space so the graph follows the GameObject
        lineRenderer.useWorldSpace = false;
    }

    /// <summary>
    /// Add a new Y-value to the graph.
    /// </summary>
    public void AddValue(float y)
    {
        if (values.Count >= pointCount)
            values.RemoveAt(0);

        values.Add(y);
        totalPointsAdded++;
        UpdateGraph();
    }

    /// <summary>
    /// Updates the LineRenderer positions.
    /// </summary>
    private void UpdateGraph()
    {
        lineRenderer.positionCount = values.Count;

        int startIndex = Mathf.Max(0, totalPointsAdded - pointCount);

        for (int i = 0; i < values.Count; i++)
        {
            // X: scale to fit graphWidth
            float normX = ((startIndex + i) / (float)pointCount) * graphWidth;

            // Y: scale and offset
            float normY = values[i] * graphHeight + yOffset;

            // Debug-utskrift – syns i Unity Console när spelet körs
            Debug.Log($"Point {i}: ({normX}, {normY})");

            // Use local positions now
            lineRenderer.SetPosition(i, new Vector3(normX, normY, 0f));
        }
    }
}
