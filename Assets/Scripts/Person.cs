using UnityEngine;

public class Person
{
    public int Age { get; private set; }
    public float Money { get; set; }
    public string Gender { get; private set; }
    public string HairStyle { get; private set; }
    public int WeeksSinceHaircut { get; private set; }

    private static System.Random random = new System.Random();

    public Person(int age, float money, string gender, string hairStyle)
    {
        Age = age;
        Money = money;
        Gender = gender;
        HairStyle = hairStyle;

        // Random number of weeks since the last haircut before the simulation starts
        WeeksSinceHaircut = random.Next(0, 10);
    }

    public void UpdateWeekly()
    {
        WeeksSinceHaircut++;
    }

    public bool WantsHaircut(float haircutPrice, int haircutInterval)
    {
        // A person wants a haircut if specified number of weeks has passed and money is sufficcent for the hair cut
        bool wantsHaircut = WeeksSinceHaircut >= haircutInterval && Money >= haircutPrice;

        if (wantsHaircut)
        {
            Debug.Log($"{Gender} {Age} wants a haircut (Money: {Money} €, Weeks since last haircut: {WeeksSinceHaircut})");
        }

        return wantsHaircut;
    }

    public void GetHaircut()
    {
        // Reseting the weeks since the last haircut to 0
        WeeksSinceHaircut = 0;
    }
}
