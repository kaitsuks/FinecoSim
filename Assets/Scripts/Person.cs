using UnityEngine;

/// <summary>
/// Represents one citizen in the economy.
/// Tracks money, age, gender, and haircut needs.
/// </summary>
public class Person
{
    public int Age { get; private set; }
    public float Money { get; set; }
    public string Gender { get; private set; }
    public string HairStyle { get; private set; }
    public int WeeksSinceHaircut { get; private set; }

    // Constructor
    public Person(int age, float money, string gender, string hairStyle)
    {
        Age = age;
        Money = money;
        Gender = gender;
        HairStyle = hairStyle;
        WeeksSinceHaircut = 0;
    }

    // Called every simulated week
    public void UpdateWeekly()
    {
        WeeksSinceHaircut++;
    }

    // Decide if this person wants a haircut
    public bool WantsHaircut()
    {
        return WeeksSinceHaircut >= 8;
    }

    // Reset haircut timer
    public void GetHaircut()
    {
        WeeksSinceHaircut = 0;
    }
}
