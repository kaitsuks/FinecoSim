using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public Graph graph;   // dra in din Graph i Inspectorn

    [Header("Simulation Settings")]
    public float updateInterval = 0.5f; // sekunder mellan uppdateringar

    private float timer = 0f;
    private float t = 0f; // "tid" för simuleringen
    private bool simulationRunning = false;

    void Update()
    {
        if (!simulationRunning) return; // gör ingenting förrän simulationen har startat

        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            t += 0.1f;

            // Exempel: linjär ökning
            float budget = t;

            // Exempel: sinusvåg istället
            // float budget = Mathf.Sin(t);

            if (graph != null)
                graph.AddValue(budget);
        }
    }

    // Anropas av GameController när spelaren tryckt på Simulationsknappen
    public void StartSimulation()
    {
        simulationRunning = true;
        t = 0f;
        timer = 0f;
    }
}
