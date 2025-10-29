using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    [Header("References")]
    public State state;
    public Graph graph;

    [Header("Simulation Data")]
    public List<Person> people = new List<Person>();
    public List<Company> salons = new List<Company>();

    [Header("Parameters")]
    private float price;
    private float vat;
    private int population;
    private int hairdressers;
    private int customersPer;
    private float interval;

    private float timer = 0f;
    private int currentWeek = 0;
    private bool simulationRunning = false;

    void Update()
    {
        if (!simulationRunning)
            return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            SimulateWeek();
        }
    }

    public void StartSimulation()
    {
        AssignEmployeesToSalons();
        simulationRunning = true;
        currentWeek = 0;
        Debug.Log("Simulation started!");
    }

    private void AssignEmployeesToSalons()
    {
        if (people.Count == 0)
            people = PersonFactory.CreatePeople(population);

        foreach (Company salon in salons)
        {
            salon.Employees = new List<Person>();
        }

        int employeesPerSalon = Mathf.Max(1, people.Count / Mathf.Max(1, salons.Count));
        int personIndex = 0;

        foreach (Company salon in salons)
        {
            for (int i = 0; i < employeesPerSalon && personIndex < people.Count; i++)
            {
                salon.AddEmployee(people[personIndex]);
                personIndex++;
            }
        }

        Debug.Log($"Assigned {personIndex} employees across {salons.Count} salons.");
    }

    private void SimulateWeek()
    {
        currentWeek++;
        Debug.Log($"--- Week {currentWeek} ---");

        PaySalaries();
        SimulateHaircuts();

        // Hämta och rita regeringens budgetsaldo
        if (state != null && graph != null)
        {
            float net = state.GetNetBudget();
            graph.AddValue(net);
            Debug.Log($"[Week {currentWeek}] Added new net budget: {net}");
        }
    }

    private void PaySalaries()
    {
        foreach (Person p in people)
        {
            if (!p.WorksAtSalon)
                p.ReceiveSalary();
        }
    }

    private void SimulateHaircuts()
    {
        // Här kan du simulera kunder som betalar pris + moms
        float totalRevenue = 0f;

        foreach (Company salon in salons)
        {
            int customers = customersPer;
            float priceWithVAT = price * (1f + vat);
            float revenue = customers * priceWithVAT;
            totalRevenue += revenue;

            if (state != null)
            {
                float tax = customers * price * vat;
                state.CollectTax(tax);
            }
        }

        Debug.Log($"Total haircut revenue this week: {totalRevenue:F2} €");
    }

    public void InitializeSimulation(float price, float vat, int population, int hairdressers, int customersPer, float interval)
    {
        this.price = price;
        this.vat = vat;
        this.population = population;
        this.hairdressers = hairdressers;
        this.customersPer = customersPer;
        this.interval = interval;

        Debug.Log($"Simulation initialized with Price: {price}, VAT: {vat}, Population: {population}, Hairdressers: {hairdressers}, Customers per Hairdresser: {customersPer}, Interval: {interval}");
    }
}
