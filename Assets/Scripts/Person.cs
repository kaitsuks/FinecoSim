using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    // Lägg till egenskaper som tidigare
    public float Money { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string HairStyle { get; set; }
    public bool WorksAtSalon { get; set; }

    public Person(int age, float money, string gender, string hairStyle)
    {
        Age = age;
        Money = money;
        Gender = gender;
        HairStyle = hairStyle;
        WorksAtSalon = false;  // standard value: an agent does not work a hair salon
    }

    public void ReceiveSalary()
    {
        // if the person is not working at the hair salon
        if (!WorksAtSalon)
        {
            float salary = Random.Range(1800f, 3000f);  // random wage between 1800 och 3000 euro
            Money += salary;
            Debug.Log($"{Gender} {Age} received a salary of {salary:F2} € (Total money: {Money:F2} €)");
        }
    }
}
