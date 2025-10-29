using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; }

    // Metod för att lägga till en anställd
    public void AddEmployee(Person employee)
    {
        Employees.Add(employee);
        employee.WorksAtSalon = true;
    }
}
