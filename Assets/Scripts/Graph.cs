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
    public int pointCount = 50;
    public float graphWidth = 10f;
    public float graphHeight = 5f;
    public float yOffset = 0f;

    [Header("Anchor")]
    public Transform anchorPoint; // <- Din nollpunkt i scenen

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
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.useWorldSpace = true; // OBS: nu världsrums-koordinater

        Debug.Log("The graph has started to be drawn");
    }

    public void AddValue(float y)
    {
        if (values.Count >= pointCount)
            values.RemoveAt(0);

        values.Add(y);
        totalPointsAdded++;

        Debug.Log($"Graph.AddValue() – new value {y}, totalPointsAdded={totalPointsAdded}");
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        if (anchorPoint == null)
        {
            Debug.LogWarning("No anchor point set for Graph!");
            return;
        }

        lineRenderer.positionCount = values.Count;

        int startIndex = Mathf.Max(0, totalPointsAdded - pointCount);

        for (int i = 0; i < values.Count; i++)
        {
            float normX = ((startIndex + i) / (float)pointCount) * graphWidth;
            float normY = values[i] * graphHeight + yOffset;

            // Punkterna placeras relativt anchorPoint
            Vector3 pos = anchorPoint.position + new Vector3(normX, normY, 0f);

            Debug.Log($"Point {i}: {pos}");
            lineRenderer.SetPosition(i, pos);
        }
    }
}
