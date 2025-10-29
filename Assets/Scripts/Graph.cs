using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(UILineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Text Labels")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI legendText;
    public RectTransform labelContainer;
    public TextMeshProUGUI labelPrefab;

    [Header("Label Settings")]
    public int labelInterval = 2;

    [Header("Graph Settings")]
    public int pointCount = 50;
    public float yOffset = 0f;
    public int scrollStep = 1; // how many points to shift left per update

    [Header("Label Appearance")]
    public float labelFontSize = 18f;
    public Vector2 labelSize = new Vector2(40f, 20f);

    [Header("Average Line (Middle Line)")]
    public Color averageLineColor = Color.yellow;
    public float averageLineThickness = 1.5f;
    public TextMeshProUGUI avgLabelPrefab;

    private UILineRenderer lineRenderer;
    private UILineRenderer avgLineRenderer;
    private TextMeshProUGUI avgLabel;

    private List<float> values = new List<float>();
    private int totalPointsAdded = 0;
    private List<TextMeshProUGUI> xLabels = new List<TextMeshProUGUI>();
    private RectTransform graphPanelRect;

    void Start()
    {
        if (titleText != null)
            titleText.text = "Government Net Budget";

        if (legendText != null)
            legendText.text = "Net budget over time";

        lineRenderer = GetComponent<UILineRenderer>();
        lineRenderer.color = Color.red;
        lineRenderer.LineThickness = 2f;
        lineRenderer.LineList = false;

        GameObject avgLineObj = new GameObject("AvgLine");
        avgLineObj.transform.SetParent(transform.parent, false);
        avgLineRenderer = avgLineObj.AddComponent<UILineRenderer>();
        avgLineRenderer.color = averageLineColor;
        avgLineRenderer.LineThickness = averageLineThickness;
        avgLineRenderer.LineList = true;

        if (avgLabelPrefab != null)
        {
            avgLabel = Instantiate(avgLabelPrefab, transform.parent);
            avgLabel.text = "";
        }

        graphPanelRect = GetComponentInParent<RectTransform>();

        // create empty x-axis labels
        for (int i = 0; i < pointCount; i++)
        {
            TextMeshProUGUI lbl = Instantiate(labelPrefab, labelContainer);
            lbl.text = "";
            RectTransform rt = lbl.GetComponent<RectTransform>();
            rt.sizeDelta = labelSize;
            lbl.fontSize = labelFontSize;
            xLabels.Add(lbl);
        }
    }

    /// <summary>
    /// Adds a new value and updates the graph.
    /// </summary>
    public void AddValue(float y)
    {
        // add the new value
        values.Add(y);
        totalPointsAdded++;

        // if we exceeded maximum count -> remove oldest
        if (values.Count > pointCount)
        {
            for (int i = 0; i < scrollStep; i++)
            {
                if (values.Count > pointCount)
                    values.RemoveAt(0);
            }
        }

        UpdateGraph();
    }

    /// <summary>
    /// Redraws the entire line and average line.
    /// </summary>
    private void UpdateGraph()
    {
        if (values.Count == 0 || graphPanelRect == null || lineRenderer == null)
            return;

        float graphWidth = graphPanelRect.rect.width;
        float graphHeight = graphPanelRect.rect.height;

        // 1) Compute average
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        // 2) Determine min/max and normalize
        float minVal = Mathf.Min(values.ToArray());
        float maxVal = Mathf.Max(values.ToArray());
        float range = Mathf.Max(1f, maxVal - minVal);

        // 3) Build the line points
        Vector2[] points = new Vector2[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            float normX = (i / (float)(pointCount - 1)) * graphWidth;

            // Value relative to average, centered vertically
            float relativeY = (values[i] - avg) / range;
            float normY = relativeY * (graphHeight / 2f);

            points[i] = new Vector2(normX - graphWidth / 2f, normY);

            // update x-labels
            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX - graphWidth / 2f, -40f);
                xLabels[i].text = (totalPointsAdded - values.Count + i + 1) % labelInterval == 0
                    ? $"{totalPointsAdded - values.Count + i + 1}"
                    : "";
            }
        }

        // 4) Update line
        lineRenderer.Points = points;
        lineRenderer.SetAllDirty();

        // 5) Update average line (middle)
        Vector2[] avgPoints = new Vector2[2];
        avgPoints[0] = new Vector2(-graphWidth / 2f, 0);
        avgPoints[1] = new Vector2(graphWidth / 2f, 0);
        avgLineRenderer.Points = avgPoints;
        avgLineRenderer.SetAllDirty();

        // 6) Update average label
        if (avgLabel != null)
        {
            avgLabel.text = $"Avg: {avg:F2}";
            avgLabel.rectTransform.anchoredPosition = new Vector2(-graphWidth / 2f - 60f, 0f);
        }
    }
}
