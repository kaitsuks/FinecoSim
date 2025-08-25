// This file defines the Person class, which represents a single agent in the simulation.
// Each Person has attributes (age, money, preferences, etc.) that influence behavior.

using System;
// Imports base .NET system utilities like Random, Console, etc.

public class Person
// Declares the Person class. Each object of this type is one simulated person.
{
    // Properties represent the agent's traits and economic state.

    // The age of the person (not very important in the first version).
    public int Age { get; set; }

    // How much money the person currently has.
    public float Money { get; set; }

    // Gender attribute, used to model different consumption patterns.
    public string Gender { get; set; }

    // The current hairstyle state of the person (short, medium, long).
    public string HairStyle { get; set; }

    // Tracks how many weeks since last haircut (used in haircut decisions).
    public int WeeksSinceHaircut { get; set; }

    // Constructor initializes a new Person with default values.
    public Person(int age, float money, string gender, string hairStyle)
    {
        // Assign the provided age to the Age property.
        Age = age;

        // Assign the provided money value to the Money property.
        Money = money;

        // Assign the provided gender string to the Gender property.
        Gender = gender;

        // Assign the provided hairstyle string to the HairStyle property.
        HairStyle = hairStyle;

        // Start haircut counter at 0 when a new person is created.
        WeeksSinceHaircut = 0;
    }

    // Method that simulates one "tick" of time for this person (e.g., a week).
    public void UpdateWeekly()
    {
        // Every week, the counter since last haircut increases by one.
        WeeksSinceHaircut++;
    }

    // Determines if the person wants a haircut this week.
    public bool WantsHaircut()
    {
        // Return true if it's been 8 or more weeks since last haircut.
        return WeeksSinceHaircut >= 8;
    }

    // Called when the person actually gets a haircut.
    public void GetHaircut()
    {
        // Reset the haircut counter back to 0 weeks.
        WeeksSinceHaircut = 0;
    }
}
