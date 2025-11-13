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

    public float hair; //hairlength
    public bool WorksAtSalon { get; set; } // Flagga som visar om personen arbetar på salongen

    public FollowTarget followTarget;
    public bool hairDresserON;

    private void Start()
    {
        followTarget = gameObject.GetComponent<FollowTarget>();
        followTarget.enabled = false;
    }

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

    private void Update()
    {
        //public static float Distance(Vector2 a, Vector2 b);
        if(Vector2.Distance(this.transform.position, followTarget.target.transform.position) < 1f) { followTarget.enabled = false;
            //hair = 1f;
            hairDresserON = true;
        }
        //if (hair > 4f) { followTarget.enabled = true; }
        //if (hair  < 2f) { followTarget.enabled = false; }
    }
}
