using System.Collections;
using System.Collections.Generic;
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

    PersonFactory personFactory;

    // Starta spelet och fördela anställda på salongerna
    void Start()
    {
        //personFactory = new PersonFactory();
        personFactory = GameObject.Find("Game").GetComponent<PersonFactory>();
        people = personFactory.CreatePeople(300);
        foreach(GameObject p in people)
        {
            Debug.Log("Person gender: " + p.gameObject.GetComponent<Person>().Gender + ", age " + p.gameObject.GetComponent<Person>().Age);
        }

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
        SimulateHaircuts();

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
    private void SimulateHaircuts()
    {
        // Din kod för att hantera hårklippningar här (om du vill)
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
}
