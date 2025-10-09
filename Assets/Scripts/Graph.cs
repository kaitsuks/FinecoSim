using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI.Extensions; // viktigt för UI Line Renderer

[RequireComponent(typeof(UILineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Text Labels")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI legendText;
    public RectTransform labelContainer;
    public TextMeshProUGUI labelPrefab;

    [Header("Graph Settings")]
    public int pointCount = 50;
    public float yOffset = 0f;

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

        // Hämta UI Line Renderer-komponenten
        lineRenderer = GetComponent<UILineRenderer>();

        // Anpassa standardinställningar (valfritt)
        lineRenderer.color = Color.red;
        lineRenderer.LineThickness = 2f; // motsvarar tjocklek i pixelvärlden
        lineRenderer.LineList = false; // kontinuerlig linje

        // Hämta GraphPanel (föräldern)
        graphPanelRect = GetComponentInParent<RectTransform>();

        // Skapa etiketter
        for (int i = 0; i < pointCount; i++)
        {
            TextMeshProUGUI lbl = Instantiate(labelPrefab, labelContainer);
            lbl.text = "";
            xLabels.Add(lbl);
        }
    }

    public void AddValue(float y)
    {
        if (values.Count >= pointCount)
            values.RemoveAt(0);

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

        // Beräkna medelvärdet
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        // Skapa positionsarray för UI Line Renderer
        Vector2[] points = new Vector2[values.Count];

        for (int i = 0; i < values.Count; i++)
        {
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = ((values[i] - avg) / graphHeight) * graphHeight + yOffset;

            points[i] = new Vector2(normX - graphWidth / 2f, normY);

            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition =
                    new Vector2(normX - graphWidth / 2f, -40f); // etikettens position
                xLabels[i].text = (i + 1).ToString();
            }
        }

        // Tilldela punkterna till UI Line Renderer
        lineRenderer.Points = points;
        lineRenderer.SetAllDirty(); // uppdatera renderingen
    }
}
