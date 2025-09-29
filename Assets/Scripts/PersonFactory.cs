using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates many Person objects with randomized attributes.
/// </summary>
public static class PersonFactory
{
    private static System.Random random = new System.Random();

    public static List<Person> CreatePeople(int count)
    {
        List<Person> people = new List<Person>();

        string[] genders = { "Male", "Female" };
        string[] hairStyles = { "Short", "Medium", "Long" };

        for (int i = 0; i < count; i++)
        {
            int age = random.Next(18, 66);
            float money = random.Next(100, 1001);
            string gender = genders[random.Next(genders.Length)];
            string hairStyle = hairStyles[random.Next(hairStyles.Length)];

            // Slumpad fåfänga mellan 0.0 och 1.0
            float vanity = (float)random.NextDouble();

            people.Add(new Person(age, money, gender, hairStyle, vanity));
        }

        return people;
    }
}
