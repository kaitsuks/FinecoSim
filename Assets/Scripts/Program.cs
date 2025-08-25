// This file contains the entry point (Main method).
// It sets up and runs the simulation.

using System;
// For Console utilities.

class Program
// Declares the Program class (required for C# entry point).
{
    // Main method, execution starts here.
    static void Main(string[] args)
    {
        // Print a title for the simulation.
        Console.WriteLine("Economic Simulation: Restaurants & Hairdressing");

        // Create a SimulationController with 300 people and 20% VAT.
        SimulationController sim = new SimulationController(300, 0.20f);

        // Run the simulation for 10 weeks as a test.
        sim.Run(10);

        // Print a closing message.
        Console.WriteLine("Simulation finished.");
    }
}