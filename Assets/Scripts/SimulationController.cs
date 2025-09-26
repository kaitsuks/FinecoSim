using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public Graph graph;
    public State government;
    public List<Hairdresser> hairdressers = new List<Hairdresser>();
    public List<Person> people = new List<Person>();

    [Header("Settings")]
    public float updateInterval = 1f; // en vecka per "tick"

    private float timer = 0f;
    private bool simulationRunning = false;
    private int week = 0;

    void Update()
    {
        if (!simulationRunning) return;

        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0f;
            RunWeek();
        }
    }

    void RunWeek()
    {
        week++;
        foreach (var p in people) p.UpdateWeekly();
        foreach (var h in hairdressers) h.ResetWeek();

        // försök ge alla personer frisörtjänst
        foreach (var p in people)
        {
            foreach (var h in hairdressers)
            {
                h.ServeCustomer(p, government.vatHairdresser);
            }
        }

        // exempel: rita genomsnittlig hår-längd
        float avgHair = 0f;
        foreach (var p in people) avgHair += p.WeeksSinceHaircut;
        avgHair /= people.Count;

        if (graph != null)
            graph.AddValue(avgHair);
    }

    public void StartSimulation()
    {
        simulationRunning = true;
        timer = 0f;
        week = 0;
    }
}
