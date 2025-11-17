using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }

    public float barberIncome = 10f; //start capital
    
    float barberExpenses = 0.0001f;

    public bool bankruptcy = false;

    //SimulationController sim;

    private void Start()
    {
        //sim = GameObject.Find("Game").GetComponent<SimulationController>();
    }

    // Metod för att lägga till en anställd
    public void AddEmployee(Person employee)
    {
        Employees.Add(employee);
        employee.WorksAtSalon = true;  // Markera att personen jobbar här
    }

    //public void AddEmployee(Person employee)
    //{
    //    Employees.Add(employee);  // Lägg till personen i listan
    //    employee.WorksAtSalon = true;  // Markera att personen jobbar här
    //}

    private void Update()
    {
        barberIncome -= barberExpenses;
        if (barberIncome < 0) { bankruptcy = true; Debug.Log("Barber " + this + " bankrupted! "); }
        if(bankruptcy)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

    }
}
