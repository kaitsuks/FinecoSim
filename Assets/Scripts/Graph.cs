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
        while (values.Count >= pointCount)
        {
            for (int i = 0; i < scrollStep && values.Count > 0; i++)
                values.RemoveAt(0);
        }

        values.Add(y);
        totalPointsAdded++;

        UpdateGraph();
    }

    private void UpdateGraph()
    {
        if (values.Count == 0 || graphPanelRect == null || lineRenderer == null)
            return;

        float graphWidth = graphPanelRect.rect.width;
        float graphHeight = graphPanelRect.rect.height;

        // counting average value
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        // drawing the main graph
        Vector2[] points = new Vector2[values.Count];
        for (int i = 0; i < values.Count; i++)
        {
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = ((values[i] - avg) / graphHeight) * graphHeight + yOffset;

            points[i] = new Vector2(normX - graphWidth / 2f, normY);

            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX - graphWidth / 2f, -40f);
                xLabels[i].text = (i % labelInterval == 0) ? $"{totalPointsAdded + i * 2}" : "";
            }
        }

        lineRenderer.Points = points;
        lineRenderer.SetAllDirty();

        // drawing the middle line always in the middle of the graph
        Vector2[] avgPoints = new Vector2[2];
        avgPoints[0] = new Vector2(-graphWidth / 2f, 0); // 0 = mitten
        avgPoints[1] = new Vector2(graphWidth / 2f, 0);
        avgLineRenderer.Points = avgPoints;
        avgLineRenderer.SetAllDirty();

        // Updating the etiket every second time
        if (avgLabel != null)
        {
            avgLabel.text = (totalPointsAdded % 2 == 0) ? $"Avg: {avg:F2}" : avgLabel.text;
            avgLabel.rectTransform.anchoredPosition = new Vector2(-420, 0f); // the location of the lable
        }
    }
}
