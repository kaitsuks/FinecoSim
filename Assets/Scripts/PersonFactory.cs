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

            people.Add(new Person(age, money, gender, hairStyle));
        }

        return people;
    }
}
