public class Person
{
    public int Age { get; private set; }
    public float Money { get; set; }
    public string Gender { get; private set; }
    public string HairStyle { get; private set; }
    public int WeeksSinceHaircut { get; private set; }

    public float Vanity { get; private set; } // 0.0 - 1.0

    public Person(int age, float money, string gender, string hairStyle, float vanity)
    {
        Age = age;
        Money = money;
        Gender = gender;
        HairStyle = hairStyle;
        WeeksSinceHaircut = 0;
        Vanity = vanity;
    }

    public void UpdateWeekly()
    {
        WeeksSinceHaircut++;
    }

    public bool WantsHaircut(float haircutPrice)
    {
        // ex: mer fåfänga → kortare max väntetid
        int maxWeeks = Mathf.RoundToInt(12 - Vanity * 8); // mellan 4–12 veckor
        return WeeksSinceHaircut >= maxWeeks && Money >= haircutPrice;
    }

    public void GetHaircut()
    {
        WeeksSinceHaircut = 0;
    }
}
