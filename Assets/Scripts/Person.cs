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
    public bool WorksAtSalon { get; set; } // Flagga som visar om personen arbetar på salongen

    //public Person(int age, float money, string gender, string hairStyle)
    //{
    //    Age = age;
    //    Money = money;
    //    Gender = gender;
    //    HairStyle = hairStyle;
    //    WorksAtSalon = false;  // Standardvärde: personen arbetar inte på salongen från början
    //}

    // Metod för att ge lön till personer som inte arbetar på salong
    public void ReceiveSalary()
    {
        // Om personen inte arbetar på salongen, ge dem en slumpmässig lön
        if (!WorksAtSalon)
        {
            float salary = Random.Range(1800f, 3000f);  // Slumpmässig lön mellan 1800 och 3000 euro
            Money += salary;
            Debug.Log($"{Gender} {Age} received a salary of {salary:F2} € (Total money: {Money:F2} €)");
        }
    }
}
