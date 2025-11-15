using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }

    public float barberIncome = 10f;
    
    float barberExpenses = 0.1f;

    public bool bankrupcy = false;

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
        //barberExpenses -= 0.0001f;
        barberIncome -= barberExpenses;
        if (barberIncome < 0) { bankrupcy = true; Debug.Log("Barber " + this + " bankrupted! "); }

    }
}
