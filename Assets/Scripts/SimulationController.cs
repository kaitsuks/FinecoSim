using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public Graph graph;   // drag your Graph object here in the Inspector

    [Header("Simulation Settings")]
    public float updateInterval = 0.5f; // seconds between updates
    private float timer = 0f;

    private float t = 0f; // "time" counter for simulation

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            t += 0.1f;

            float budget = Mathf.Sin(t);

            // Add only the y-value to the graph
            if (graph != null)
                graph.AddValue(budget);
        }
    }
}
