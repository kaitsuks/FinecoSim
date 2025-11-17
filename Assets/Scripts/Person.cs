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
    public Wander wander;
    public bool hairDresserON;
    public bool isWandering = true;
    public bool isStopped = false;

    private void Start()
    {
        followTarget = gameObject.GetComponent<FollowTarget>();
        wander = gameObject.GetComponent<Wander>();
        wander.enabled = true;
        followTarget.enabled = false;
        //Invoke("DisableFollowTarget", 1f);
    }

    void DisableFollowTarget()
    {
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

    public void SitInBarbershop()
    {
        //wander.enabled = false;
        isWandering = false;
        StartCoroutine(DelayedHairCut());
    }

    IEnumerator DelayedHairCut()
     {
        float barberTimeStart = Time.time;
        //Debug.Log("Time 1. " + Time.time);
        followTarget.enabled = false;

        yield return new WaitForSeconds(5f);
        //followTarget.enabled = true;
        //wander.enabled = true;
        hairDresserON = false;
        float barberTimeEnd = Time.time;
        //Debug.Log("Time 2. " + Time.time);
        //isWandering = true;
        isWandering = true;
        isStopped = false;
        Debug.Log("Barber Time TOTAL " + (barberTimeEnd - barberTimeStart));
    }

    private void Update()
    {
        //public static float Distance(Vector2 a, Vector2 b);
        if(Vector2.Distance(this.transform.position, followTarget.target.transform.position) < 1f) { followTarget.enabled = false;
            //hair = 1f;
            hairDresserON = true; //go to barbershop
            isStopped = true;
        }
        //if (!hairDresserON) { followTarget.enabled = false; }
        //if (hairDresserON) { followTarget.enabled = true; }
    }
}
