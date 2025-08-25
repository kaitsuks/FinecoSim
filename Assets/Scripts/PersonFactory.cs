// This file defines PersonFactory, which creates many Person objects at once.
// It uses "pools" of possible attribute values and randomly assigns them.

using System;
// Gives access to Random and Console.

using System.Collections.Generic;
// Needed for using List<T> to store agents.

public static class PersonFactory
// Declares a static class (no instances) responsible for generating persons.
{
    // Static Random object used for all random selections.
    private static Random random = new Random();

    // Method that generates 'count' number of Person objects.
    public static List<Person> CreatePeople(int count)
    {
        // Create a new list to hold all generated persons.
        List<Person> people = new List<Person>();

        // Define possible genders in a pool.
        string[] genders = { "Male", "Female" };

        // Define possible hairstyles in a pool.
        string[] hairStyles = { "Short", "Medium", "Long" };

        // Loop runs 'count' times to create that many persons.
        for (int i = 0; i < count; i++)
        {
            // Pick a random age between 18 and 65.
            int age = random.Next(18, 66);

            // Give the person some starting money between 100 and 1000.
            float money = random.Next(100, 1001);

            // Pick a random gender from the gender pool.
            string gender = genders[random.Next(genders.Length)];

            // Pick a random hairstyle from the hairstyle pool.
            string hairStyle = hairStyles[random.Next(hairStyles.Length)];

            // Create a new Person object with chosen attributes.
            Person person = new Person(age, money, gender, hairStyle);

            // Add the new person to the list of people.
            people.Add(person);
        }

        // Return the full list of created persons.
        return people;
    }
}
