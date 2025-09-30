using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
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

    private LineRenderer lineRenderer;
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

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.widthMultiplier = 0.02f; //thickness of line
        lineRenderer.useWorldSpace = false;

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
        if (values.Count == 0) return;

        float graphWidth = graphPanelRect.rect.width;
        float graphHeight = graphPanelRect.rect.height;

        // Medelvärdet
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        lineRenderer.positionCount = values.Count;

        for (int i = 0; i < values.Count; i++)
        {
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = ((values[i] - avg) / graphHeight) * graphHeight + yOffset;

            Vector3 pos = new Vector3(normX - graphWidth / 2, normY, 0f);
            lineRenderer.SetPosition(i, pos);

            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX - graphWidth / 2, -40f); //the distance to the graph screen
                xLabels[i].text = (i + 1).ToString(); //what is written
            }
        }
    }
}
