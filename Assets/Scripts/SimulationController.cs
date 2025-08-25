// This file runs the actual simulation loop.
// It controls time progression, applies taxes, and gathers results.

using System;
// For Console output and basic utilities.

using System.Collections.Generic;
// For using List<Person>.

public class SimulationController
// Declares the SimulationController class, main manager of the sim.
{
    // List of all agents (persons) in the simulation.
    private List<Person> people;

    // Current VAT rate (Value-Added Tax) applied to spending.
    private float vatRate;

    // Tracks total tax revenue collected by the government.
    private float taxRevenue;

    // Constructor initializes the simulation with a given number of people.
    public SimulationController(int populationSize, float initialVatRate)
    {
        // Create the agents using PersonFactory.
        people = PersonFactory.CreatePeople(populationSize);

        // Store the VAT rate.
        vatRate = initialVatRate;

        // Start with zero tax revenue.
        taxRevenue = 0f;
    }

    // Runs the simulation for a given number of weeks.
    public void Run(int weeks)
    {
        // Loop over the number of weeks to simulate.
        for (int week = 1; week <= weeks; week++)
        {
            // Write a message showing which week is running.
            Console.WriteLine($"--- Week {week} ---");

            // Process weekly behavior of all agents.
            SimulateWeek();

            // Show government’s current tax revenue after this week.
            Console.WriteLine($"Cumulative Tax Revenue: {taxRevenue}");
        }
    }

    // Handles weekly events like haircuts and restaurant visits.
    private void SimulateWeek()
    {
        // Loop through each person in the population.
        foreach (var person in people)
        {
            // Update their internal counters (e.g., weeks since haircut).
            person.UpdateWeekly();

            // If this person wants a haircut:
            if (person.WantsHaircut())
            {
                // Define the base cost of a haircut.
                float haircutPrice = 20f;

                // Add VAT to the base price.
                float totalPrice = haircutPrice * (1 + vatRate);

                // If the person can afford it:
                if (person.Money >= totalPrice)
                {
                    // Deduct the cost from their money.
                    person.Money -= totalPrice;

                    // Reset haircut counter.
                    person.GetHaircut();

                    // Add the VAT portion to tax revenue.
                    taxRevenue += haircutPrice * vatRate;

                    // Print a message about the haircut event.
                    Console.WriteLine($"{person.Gender} aged {person.Age} got a haircut. Remaining money: {person.Money}");
                }
            }
        }
    }
}
