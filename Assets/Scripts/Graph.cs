using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI.Extensions; // för UILineRenderer

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
    public int scrollStep = 2;

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
            legendText.text = "y over time";

        // the main graph
        lineRenderer = GetComponent<UILineRenderer>();
        lineRenderer.color = Color.red;
        lineRenderer.LineThickness = 2f;
        lineRenderer.LineList = false;

        // the line for the average value
        GameObject avgLineObj = new GameObject("AvgLine");
        avgLineObj.transform.SetParent(transform.parent, false);
        avgLineRenderer = avgLineObj.AddComponent<UILineRenderer>();
        avgLineRenderer.color = averageLineColor;
        avgLineRenderer.LineThickness = averageLineThickness;
        avgLineRenderer.LineList = true;

        // label for the average value
        if (avgLabelPrefab != null)
        {
            avgLabel = Instantiate(avgLabelPrefab, transform.parent);
            avgLabel.text = "";
        }

        // reference to the parent (GraphPanel)
        graphPanelRect = GetComponentInParent<RectTransform>();

        // creating eticets under the graph
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

    public void AddValue(float y)
    {
        // removing old values if above maximal number        while (values.Count >= pointCount)
        {
            for (int i = 0; i < scrollStep && values.Count > 0; i++)
                values.RemoveAt(0);
        }

        // adding a new value
        values.Add(y);
        totalPointsAdded++;

        // drawing a graph
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        if (values.Count == 0 || graphPanelRect == null || lineRenderer == null)
            return;

        float graphWidth = graphPanelRect.rect.width;
        float graphHeight = graphPanelRect.rect.height;

        // counting the average value
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        // finding minimmal and maximum values for scaling of graph
        float minVal = Mathf.Min(values.ToArray());
        float maxVal = Mathf.Max(values.ToArray());
        float range = Mathf.Max(1f, maxVal - minVal);

        // building points for the line
        Vector2[] points = new Vector2[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            // X: propotionally along the width of the graph
            float normX = (i / (float)(pointCount - 1)) * graphWidth;

            // Y: in relation to the average value (0 = middle)
            float relativeY = (values[i] - avg) / range; // in between -0.5 and 0.5 roughly
            float normY = relativeY * (graphHeight / 2f); // middle = 0

            // the first point, always start in the middle (0,0)
            if (i == 0)
                normY = 0;

            points[i] = new Vector2(normX - graphWidth / 2f, normY);

            // labels under the graph
            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX - graphWidth / 2f, -40f);
                xLabels[i].text = (i % labelInterval == 0) ? $"{totalPointsAdded + i * 2}" : "";
            }
        }

        // updating the line
        lineRenderer.Points = points;
        lineRenderer.SetAllDirty();

        // drawing the line in the middle
        Vector2[] avgPoints = new Vector2[2];
        avgPoints[0] = new Vector2(-graphWidth / 2f, 0);
        avgPoints[1] = new Vector2(graphWidth / 2f, 0);
        avgLineRenderer.Points = avgPoints;
        avgLineRenderer.SetAllDirty();

        // Showing average value to left of (0, middle)
        if (avgLabel != null)
        {
            avgLabel.text = $"Avg: {avg:F2}";
            avgLabel.rectTransform.anchoredPosition = new Vector2(-graphWidth / 2f - 60f, 0f);
        }
    }
}
