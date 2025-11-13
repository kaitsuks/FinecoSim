    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Creates many Person objects with randomized attributes.
    /// </summary>
    public class PersonFactory : MonoBehaviour
{
        private static System.Random random = new System.Random();
    GameObject personPrefab;
    public GameObject personPrefab1;
    public GameObject personPrefab2;
    public GameObject personPrefab3;
    Vector2 place;
    float hair;
    Vector3 hairV3; //hair length vector3

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
            //place.x = random.Next(100, 100);
            //place.y = random.Next(100, 100);

            place.x = Random.Range(-10f, 10f);
            place.y = Random.Range(-10f, 10f);

            float money = random.Next(100, 1001);
            string gender = genders[random.Next(genders.Length)];
            string hairStyle = hairStyles[random.Next(hairStyles.Length)];
            //people.Add(new Person(age, money, gender, hairStyle)); // Anropar den nya konstruktorn
           
            //GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            //GameObject p = GameObject.Instantiate(personPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            //if(Random.Range(0, 2) == 0) { personPrefab = personPrefab2; } //else { personPrefab = personPrefab3; }
            GameObject p = GameObject.Instantiate(personPrefab2, new Vector3( place.x, place.y, 10f), Quaternion.identity) as GameObject;
            //(age, money, gender, hairStyle));
            p.gameObject.GetComponent<Person>().Age = age;
            p.gameObject.GetComponent<Person>().Money = money;
            p.gameObject.GetComponent<Person>().Gender = gender;
            p.gameObject.GetComponent<Person>().HairStyle = hairStyle;
            p.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            p.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.6f, 1f), Random.Range(0.6f, 1f), Random.Range(0.6f, 1f)); ;
            hair = Random.Range(1f, 10f);
            p.gameObject.GetComponent<Person>().hair = hair;
            hairV3 = new Vector3(1f, hair, 1f);
            p.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().transform.localScale = hairV3;
           
        people.Add(p);

            //p = GameObject.Instantiate(personPrefab2, new Vector3(place.x, place.y, 10f), Quaternion.identity) as GameObject;
            //p.gameObject.GetComponent<Person>().Age = age;
            //p.gameObject.GetComponent<Person>().Money = money;
            //p.gameObject.GetComponent<Person>().Gender = gender;
            //p.gameObject.GetComponent<Person>().HairStyle = hairStyle;

            //people.Add(p);
        }

        return people;
    }
}
