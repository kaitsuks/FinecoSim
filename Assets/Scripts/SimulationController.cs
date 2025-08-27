using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Runs the actual simulation loop in Unity.
/// Attach this script to an empty GameObject.
/// </summary>
public class SimulationController : MonoBehaviour
{
    public int populationSize = 100;
    public State government;
    public Company hairdresserCompany;

    private List<Person> people;

    void Start()
    {
        // Create population
        people = PersonFactory.CreatePeople(populationSize);

        // Link government to companies
        hairdresserCompany.government = government;

        // Run 10 weeks of simulation
        for (int week = 1; week <= 10; week++)
        {
            SimulateWeek(week);
        }

        Debug.Log("Simulation finished.");
        Debug.Log($"Government net budget: {government.GetNetBudget()}");
    }

    private void SimulateWeek(int week)
    {
        Debug.Log($"--- Week {week} ---");

        foreach (var person in people)
        {
            person.UpdateWeekly();

            if (person.WantsHaircut())
            {
                hairdresserCompany.ServeCustomer(person, government.vatHairdresser);
                person.GetHaircut();
                Debug.Log($"{person.Gender} aged {person.Age} got a haircut. Money left: {person.Money}");
            }
        }

        // Once a month, pay wages
        if (week % 4 == 0)
        {
            hairdresserCompany.PayWages(people);
            Debug.Log("Wages paid to citizens.");
        }
    }
}
