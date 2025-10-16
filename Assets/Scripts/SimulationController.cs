using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public Graph graph;

    [Header("Simulation Settings")]
    public float updateInterval = 0.5f;

    // new settings
    private float haircutPrice;
    private float vatHaircut;
    private int population;
    private int hairdressers;
    private int customersPerHairdresser;

    private float timer = 0f;
    private float t = 0f;
    private bool simulationRunning = false;

    public void InitializeSimulation(float price, float vat, int pop, int hair, int customersPer, float interval)
    {
        haircutPrice = price;
        vatHaircut = vat;
        population = pop;
        hairdressers = hair;
        customersPerHairdresser = customersPer;
        updateInterval = interval;

        Debug.Log($"Simulation initialized: price={price}, vat={vat}, pop={pop}, hair={hair}, cust/hair={customersPer}, interval={interval}");
    }

    void Update()
    {
        if (!simulationRunning) return;

        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            t += 0.1f;

            // the logic of the code is written here
            float budget = Mathf.Sin(t) * 10f + 50f;
            if (graph != null)
                graph.AddValue(budget);
        }
    }

    public void StartSimulation()
    {
        simulationRunning = true;
        t = 0f;
        timer = 0f;
    }
}