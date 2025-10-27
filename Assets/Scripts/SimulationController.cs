    using UnityEngine;
    using System.Collections.Generic;

    public class SimulationController : MonoBehaviour
    {
        [Header("References")]
        public Graph graph;

        [Header("Simulation Settings")]
        public float updateInterval = 0.5f;

        // Input values
        private float haircutPrice;
        private float vatHaircut;
        private int population;
        private int hairdressers;
        private int customersPerHairdresser;

        // Internal data
        private List<Person> people;
        private State state;
        private List<Company> salons;

        private float timer = 0f;
        private int currentWeek = 0;
        private bool simulationRunning = false;

        public void InitializeSimulation(float price, float vat, int pop, int hair, int customersPer, float interval)
        {
            haircutPrice = price;
            vatHaircut = vat;
            population = pop;
            hairdressers = hair;
            customersPerHairdresser = customersPer;
            updateInterval = interval;

            Debug.Log($"✅ Simulation initialized:\n" +
                      $"Haircut price: {price} €\n" +
                      $"VAT: {vat * 100f}%\n" +
                      $"Population: {pop}\n" +
                      $"Hairdressers: {hair}\n" +
                      $"Customers per hairdresser per day: {customersPer}\n" +
                      $"Time interval (weeks): {interval}");

            // Creating people (agents)
            people = PersonFactory.CreatePeople(population);

            // Creating the government/state
            GameObject stateObj = new GameObject("State");
            state = stateObj.AddComponent<State>();

            // Creating hair salon companies
            salons = new List<Company>();
            for (int i = 0; i < hairdressers; i++)
            {
                GameObject salonObj = new GameObject($"Salon_{i + 1}");
                Company c = salonObj.AddComponent<Company>();
                c.companyName = $"Hairdresser_{i + 1}";
                c.basePrice = haircutPrice;
                c.employees = 1;
                c.government = state;
                salons.Add(c);
            }

            Debug.Log($"💇‍♀️ Created {hairdressers} hair salon companies.");
        }

        void Update()
        {
            if (!simulationRunning) return;

            timer += Time.deltaTime;

            if (timer >= updateInterval)
            {
                timer = 0f;
                SimulateWeek();
            }
        }

    private void SimulateWeek()
    {
        currentWeek++;
        int totalHaircuts = 0;
        float totalRevenue = 0f;
        float totalVAT = 0f;

        // Simulation of all hair cuts this week
        foreach (Company salon in salons)
        {
            int possibleCustomers = Mathf.Min(customersPerHairdresser * 7, population);
            List<Person> eligibleCustomers = new List<Person>();

            // Finding all persons wanting a hair cut this week
            foreach (Person p in people)
            {
                if (p.WantsHaircut(haircutPrice, customersPerHairdresser)) // A person wants hair cut
                {
                    eligibleCustomers.Add(p);
                }
            }

            // Customers are send to salon
            int customersToServe = Mathf.Min(eligibleCustomers.Count, customersPerHairdresser * 7);

            for (int i = 0; i < customersToServe; i++)
            {
                Person customer = eligibleCustomers[Random.Range(0, eligibleCustomers.Count)];

                float totalPrice = haircutPrice * (1f + vatHaircut);
                if (customer.Money >= totalPrice)
                {
                    salon.ServeCustomer(customer, vatHaircut);
                    customer.GetHaircut();  // A person receives hair cut
                    totalHaircuts++;

                    // The customer received a hair cut
                    Debug.Log($"{customer.Gender} {customer.Age} was served a haircut for {totalPrice:F2} € (Remaining money: {customer.Money} €).");

                    totalRevenue += totalPrice;
                    totalVAT += haircutPrice * vatHaircut;
                }
                else
                {
                    // A person had too little money
                    Debug.Log($"❌ {customer.Gender} {customer.Age} could not afford a haircut (Needed: {totalPrice} €, but has: {customer.Money} €).");
                }
            }
        }


        // The state pays unemployment benefits (to around 10% of people)
        int unemployedCount = Mathf.RoundToInt(population * 0.1f);
            for (int i = 0; i < unemployedCount; i++)
            {
                Person randomUnemployed = people[Random.Range(0, people.Count)];
                state.PayUnemployment(randomUnemployed);
            }

            // Calculate the state's net budget
            float netBudget = state.GetNetBudget();

            // Update the graph
            if (graph != null)
                graph.AddValue(netBudget);

            // Log detailed weekly info
            Debug.Log(
                $"📅 Week {currentWeek}:\n" +
                $"— Haircuts performed: {totalHaircuts}\n" +
                $"— Total revenue (incl. VAT): {totalRevenue:F2} €\n" +
                $"— VAT collected: {totalVAT:F2} €\n" +
                $"— Total taxes collected: {state.totalTaxesCollected:F2} €\n" +
                $"— Total benefits paid: {state.totalBenefitsPaid:F2} €\n" +
                $"— Net government budget: {netBudget:F2} €\n"
            );
        }

        public void StartSimulation()
        {
            if (people == null || people.Count == 0)
            {
                Debug.LogError("Error: One must initialize the simulation through SimulationInputs before starting");
                return;
            }

            simulationRunning = true;
            timer = 0f;
            currentWeek = 0;

            Debug.Log("Simulation started");
        }
    }
