using System.Collections.Generic;
using UnityEngine;

public static class PersonFactory
{
    private static System.Random random = new System.Random();

    // Create people as GameObjects with Person component
    public static List<Person> CreatePeople(int count, int haircutIntervalPerPerson = 8)
    {
        List<Person> people = new List<Person>();

        string[] genders = { "Male", "Female" };
        string[] hairStyles = { "Short", "Medium", "Long" };

        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject($"Person_{i + 1}");
            Person p = go.AddComponent<Person>();

            int age = random.Next(18, 66);
            float money = random.Next(100, 1001);
            string gender = genders[random.Next(genders.Length)];
            string hairStyle = hairStyles[random.Next(hairStyles.Length)];
            p.Init(age, money, gender, hairStyle, haircutIntervalPerPerson);

            people.Add(p);
        }

        return people;
    }
}
