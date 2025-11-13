using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    //List<Person> people; // Lista över alla personer
    //List<Company> salons; // Lista över alla salonger

    List<GameObject> people;
    List<GameObject> salons;

    // De nya variablerna för att lagra de inputvärden som kommer från SimulationInputs
    private float price;
    private float vat;
    private int population;
    private int hairdressers;
    private int customersPer;
    private float interval;

    float hair;
    Vector3 hairV3;

    PersonFactory personFactory;

    // Starta spelet och fördela anställda på salongerna
    void Start()
    {
        //personFactory = new PersonFactory();
        personFactory = GameObject.Find("Game").GetComponent<PersonFactory>();
        people = personFactory.CreatePeople(300);
        //foreach(GameObject p in people)
        //{
        //    Debug.Log("Person gender: " + p.gameObject.GetComponent<Person>().Gender + ", age " + p.gameObject.GetComponent<Person>().Age);
        //    Debug.Log("Person place x: " + p.gameObject.transform.position.x + ", y " + p.gameObject.transform.position.y);
        //}

        // Tilldela anställda till salongerna
        //AssignEmployeesToSalons();
        // Simulera veckan när spelet startar
        //SimulateWeek();
    }

    public void StartSimulation()
    {
        // Här kan du kalla på Start() om du vill att Start-logiken ska köras också
        AssignEmployeesToSalons(); // Tilldela anställda
        SimulateWeek(); // Simulera veckan

        Debug.Log("Simulation started!");
    }

    // Tilldela anställda till salongerna
    private void AssignEmployeesToSalons()
    {
        //foreach (Company salon in salons)
        //{
        //    // Tilldela den första personen som arbetar på salongen
        //    if (people.Count > 0)
        //    {
        //        people[0].WorksAtSalon = true; // Markera som anställd
        //    }
        //}
    }

    // Simulera en vecka
    private void SimulateWeek()
    {
        // Simulera aktiviteter för hårklippning eller annat
        //SimulateHaircuts();

        // Ge lön till alla som inte arbetar på salongerna
        PaySalaries();
    }

    // Ge lön till alla personer som inte arbetar på en salong
    private void PaySalaries()
    {
        //foreach (Person p in people)
        //{
        //    if (!p.WorksAtSalon) // Om personen inte arbetar på en salong
        //    {
        //        p.ReceiveSalary(); // Ge dem en lön
        //    }
        //}
    }

    // Metod för att simulera hårklippningar (lägg till din kod här om du vill)
    private void SimulateHaircuts(GameObject p)
    {
        GameObject agent = p;
        // Din kod för att hantera hårklippningar här (om du vill)
        if(agent.GetComponent<Person>().Money > 0f && agent.GetComponent<Person>().hairDresserON)
        {
            //cut hair
            //agent.GetComponent<Person>().hair = 1f;
            hair = 1f;
            //take money
            agent.GetComponent<Person>().Money -= 5f;
            agent.gameObject.GetComponent<Person>().hair = hair;
            //Debug.Log("Hair CUT! = " + hair);
            agent.GetComponent<Person>().hairDresserON = false;
        }
        //else
        //{
        //    Debug.Log("OUT OF MONEY, DEAD! = " + hair);
        //    Destroy(p);
        //}

    }

    // Lägg till en metod för att ta emot inputvärden och initialisera simuleringen
    public void InitializeSimulation(float price, float vat, int population, int hairdressers, int customersPer, float interval)
    {
        this.price = price;
        this.vat = vat;
        this.population = population;
        this.hairdressers = hairdressers;
        this.customersPer = customersPer;
        this.interval = interval;

        // Här kan du göra eventuella initialiseringar för din simulering
        Debug.Log($"Simulation initialized with Price: {price}, VAT: {vat}, Population: {population}, Hairdressers: {hairdressers}, Customers per Hairdresser: {customersPer}, Interval: {interval}");
    }

    //https://discussions.unity.com/t/need-help-with-converting-an-array-to-a-list-in-c/98583/2
    private void Update()
    {
        GameObject[] peoples = GameObject.FindGameObjectsWithTag("human");
        people = peoples.ToList();

        foreach (GameObject p in people)
        {
            //Debug.Log("Person gender: " + p.gameObject.GetComponent<Person>().Gender + ", age " + p.gameObject.GetComponent<Person>().Age);
            //Debug.Log("Person place x: " + p.gameObject.transform.position.x + ", y " + p.gameObject.transform.position.y);
            hair = p.gameObject.GetComponent<Person>().hair;
            hair += 0.001f;
            if(hair > 10f ) { SimulateHaircuts(p);  }
            //Debug.Log("Hair length = " + hair);

            hairV3 = new Vector3(1f, hair, 1f);
            
            p.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().transform.localScale = hairV3;
            p.gameObject.GetComponent<Person>().hair = hair;



        }

    }
}
