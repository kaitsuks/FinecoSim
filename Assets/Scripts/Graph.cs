using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI.Extensions; // required for UI Line Renderer

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

    [Header("Label Appearance")]
    public float labelFontSize = 18f;
    public Vector2 labelSize = new Vector2(40f, 20f);

    private UILineRenderer lineRenderer;
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

        //Retrieving UI Line Renderer-component
        lineRenderer = GetComponent<UILineRenderer>();

        //settings
        lineRenderer.color = Color.red; // colour of line
        lineRenderer.LineThickness = 2f; //thicknes i millimetres
        lineRenderer.LineList = false; //continuous line

        //Retrieving GraphPanel (parent)
        graphPanelRect = GetComponentInParent<RectTransform>();

        //Creating etikets
        for (int i = 0; i < pointCount; i++)
        {
            TextMeshProUGUI lbl = Instantiate(labelPrefab, labelContainer);
            lbl.text = "";

            RectTransform rt = lbl.GetComponent<RectTransform>();
            rt.sizeDelta = labelSize; // Bringing from Inspector
            lbl.fontSize = labelFontSize; // Bringing from Inspector

            xLabels.Add(lbl);
        }
    }

    public void AddValue(float y)
    {
        if (values.Count >= pointCount)
        {
            // removal of two points instead of one
            values.RemoveAt(0);
            if (values.Count >= pointCount)
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

        // counting the average value
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        Vector2[] points = new Vector2[values.Count];

        // counting the startindex in "global" coordinates
        int firstVisibleIndex = totalPointsAdded - values.Count;

        for (int i = 0; i < values.Count; i++)
        {
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = ((values[i] - avg) / graphHeight) * graphHeight + yOffset;

            points[i] = new Vector2(normX - graphWidth / 2f, normY);

            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX - graphWidth / 2f, -40f);

                int globalIndex = totalPointsAdded - values.Count + i;

                // only every second (or according to laberInterval) is shown
                if (globalIndex % labelInterval == 0)
                    xLabels[i].text = $"{globalIndex}";
                else
                    xLabels[i].text = "";
            }
        }

        lineRenderer.Points = points;
        lineRenderer.SetAllDirty();
    }
}
