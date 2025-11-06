    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Creates many Person objects with randomized attributes.
    /// </summary>
    public class PersonFactory : MonoBehaviour
{
        private static System.Random random = new System.Random();
    public GameObject personPrefab;
    Vector2Int place;

    //public List<Person> CreatePeople(int count)
    public List<GameObject> CreatePeople(int count)
    {
        //List<Person> people = new List<Person>();
        List<GameObject> people = new List<GameObject>();


        string[] genders = { "Male", "Female" };
        string[] hairStyles = { "Short", "Medium", "Long" };

        for (int i = 0; i < count; i++)
        {
            int age = random.Next(18, 66);
            place.x = random.Next(0, 500);
            place.y = random.Next(0, 500);
            float money = random.Next(100, 1001);
            string gender = genders[random.Next(genders.Length)];
            string hairStyle = hairStyles[random.Next(hairStyles.Length)];
            //people.Add(new Person(age, money, gender, hairStyle)); // Anropar den nya konstruktorn
           
            //GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            //GameObject p = GameObject.Instantiate(personPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            GameObject p = GameObject.Instantiate(personPrefab, new Vector3(place.x, place.y), Quaternion.identity) as GameObject;
            //(age, money, gender, hairStyle));

            p.gameObject.GetComponent<Person>().Age = age;
            p.gameObject.GetComponent<Person>().Money = money;
            p.gameObject.GetComponent<Person>().Gender = gender;
            p.gameObject.GetComponent<Person>().HairStyle = hairStyle;


            people.Add(p);
        }

        return people;
    }
}
