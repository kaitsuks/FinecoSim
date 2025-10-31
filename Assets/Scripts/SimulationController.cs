using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public State state;
    public Graph graph;
    public GameObject companyPrefab; // optional, can be null

    [Header("Simulation Data")]
    public List<Person> people = new List<Person>();
    public List<Company> salons = new List<Company>();

    [Header("Parameters")]
    private float price = 20f;
    private float vat = 0.20f;
    private int population = 100;
    private int hairdressers = 5;
    private int customersPer = 5;   // customers per employee (used to set salon capacity)
    private float interval = 1f;    // seconds per simulated week
    private int haircutIntervalPerPerson = 8;

    private float timer = 0f;
    private int currentWeek = 0;
    private bool simulationRunning = false;

    void Update()
    {
        if (!simulationRunning) return;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            SimulateWeek();
        }
    }

    public void StartSimulation()
    {
        if (people == null || people.Count == 0)
            people = PersonFactory.CreatePeople(population, haircutIntervalPerPerson);

        if (salons == null || salons.Count == 0)
        {
            salons = new List<Company>();
            for (int i = 0; i < hairdressers; i++)
            {
                GameObject salonObj;
                if (companyPrefab != null)
                    salonObj = Instantiate(companyPrefab);
                else
                    salonObj = new GameObject($"Salon_{i + 1}");

                salonObj.name = $"Salon_{i + 1}";
                Company salon = salonObj.GetComponent<Company>();
                if (salon == null) salon = salonObj.AddComponent<Company>();
                salon.Name = salonObj.name;
                salons.Add(salon);
            }
            Debug.Log($"Created {salons.Count} salons.");
        }

        AssignEmployeesToSalons();
        ResetAllSalonsForWeek();

        simulationRunning = true;
        currentWeek = 0;
        Debug.Log("Simulation started!");
    }

    private void AssignEmployeesToSalons()
    {
        foreach (Company salon in salons)
            salon.Employees = new List<Person>();

        int idx = 0;
        while (idx < people.Count)
        {
            foreach (Company salon in salons)
            {
                if (idx >= people.Count) break;
                salon.AddEmployee(people[idx]);
                idx++;
            }
        }

        Debug.Log($"Assigned {idx} employees across {salons.Count} salons.");
    }

    private void ResetAllSalonsForWeek()
    {
        foreach (Company s in salons)
        {
            int capacity = Mathf.Max(1, (s.Employees?.Count ?? 1) * customersPer);
            s.weeklyCapacity = capacity;
            s.ResetWeek();
        }
    }

    private void SimulateWeek()
    {
        currentWeek++;
        Debug.Log($"--- Week {currentWeek} ---");

        foreach (Person p in people) p.DecrementHairCountdown();

        ResetAllSalonsForWeek();

        List<Person> wanting = new List<Person>();
        foreach (Person p in people)
            if (p.WantsHaircut()) wanting.Add(p);

        Debug.Log($"People wanting haircut this week: {wanting.Count}");

        int servedCount = 0;
        foreach (Company salon in salons)
        {
            for (int i = wanting.Count - 1; i >= 0; i--)
            {
                if (salon.GetRemainingCapacity() <= 0) break;

                Person candidate = wanting[i];
                bool served = salon.TryServeCustomer(candidate, price, vat, state);
                if (served)
                {
                    candidate.ReceiveHaircut();
                    wanting.RemoveAt(i);
                    servedCount++;
                }
            }
        }

        Debug.Log($"Served haircuts this week: {servedCount}. Waiting: {wanting.Count}");

        // 5) Tally revenue
        float totalRevenue = 0f;
        foreach (Company salon in salons) totalRevenue += salon.WeeklyRevenue;
        Debug.Log($"Total haircut revenue this week: {totalRevenue:F2} €");

        PaySalaries();

        if (state != null && graph != null)
        {
            float net = state.GetNetBudget();
            graph.AddValue(net);
            Debug.Log($"[Week {currentWeek}] Net budget: {net:F2}");
        }
        else
        {
            Debug.LogWarning("Graph or State not assigned!");
        }
    }

    private void PaySalaries()
    {
        foreach (Person p in people)
            if (!p.WorksAtSalon)
                p.ReceiveSalary();
    }

    public void InitializeSimulation(float price, float vat, int population, int hairdressers, int customersPer, float interval)
    {
        this.price = price;
        this.vat = vat;
        this.population = population;
        this.hairdressers = hairdressers;
        this.customersPer = customersPer;
        this.interval = interval;

        Debug.Log($"Simulation initialized with Price: {price}, VAT: {vat}, Population: {population}, Hairdressers: {hairdressers}, CustomersPer: {customersPer}, Interval: {interval}");
    }

    public void ResetGraph()
    {
        if (graph != null)
        {
            graph.ClearGraph();
        }

        Debug.Log("Graph data is reset");
    }
}
