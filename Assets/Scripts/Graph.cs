using UnityEngine;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class Graph : MonoBehaviour
{
    [Header("Text Labels")]
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI legendText;
    public RectTransform labelContainer;   // an empty game object under the Canvas for eticets
    public TextMeshProUGUI labelPrefab;    // prefabricated object for X-eticets

    [Header("Graph Settings")]
    public int pointCount = 50;        // max number visible points at one time
    public float graphWidth = 500f;    // in UI-pixels
    public float graphHeight = 200f;   // in UI-pixels
    public float yOffset = 0f;

    private LineRenderer lineRenderer;
    private List<float> values = new List<float>();
    private int totalPointsAdded = 0;
    private List<TextMeshProUGUI> xLabels = new List<TextMeshProUGUI>();

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
        lineRenderer.widthMultiplier = 2f;
        lineRenderer.useWorldSpace = false;

        // creating eticts for the x-axis
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
        if (values.Count == 0)
            return;

        // counting the average value
        float sum = 0f;
        foreach (float v in values) sum += v;
        float avg = sum / values.Count;

        lineRenderer.positionCount = values.Count;

        for (int i = 0; i < values.Count; i++)
        {
            // X-coordinat (pixels → normalised length in World Space)
            float normX = (i / (float)(pointCount - 1)) * graphWidth;
            float normY = ((values[i] - avg) / graphHeight) * graphHeight + yOffset;

            Vector3 pos = new Vector3(normX, normY, 0f);
            lineRenderer.SetPosition(i, pos);

            // updating x-etikets position and text
            if (i < xLabels.Count)
            {
                xLabels[i].rectTransform.anchoredPosition = new Vector2(normX, -20f); // -20 under the graph
                int weekNumber = totalPointsAdded - values.Count + i + 1;
                xLabels[i].text = "v." + weekNumber;
            }
        }
    }
}
