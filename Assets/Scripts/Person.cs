using UnityEngine;

public class Person : MonoBehaviour
{
    public float Money { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string HairStyle { get; set; }
    public bool WorksAtSalon { get; set; }

    // Haircut logic
    public int HaircutInterval = 8; // veckor mellan klipp
    public int hairCountdown = 0;    // när <= 0 => vill ha klipp

    public void Init(int age, float money, string gender, string hairStyle, int haircutInterval)
    {
        Age = age;
        Money = money;
        Gender = gender;
        HairStyle = hairStyle;
        WorksAtSalon = false;
        HaircutInterval = Mathf.Max(1, haircutInterval);
        hairCountdown = Random.Range(0, HaircutInterval); // slumpmässig start
    }

    public void ReceiveSalary()
    {
        if (!WorksAtSalon)
        {
            float salary = Random.Range(1800f, 3000f);
            Money += salary;
            Debug.Log($"{Gender} {Age} received a salary of {salary:F2} € (Total money: {Money:F2} €)");
        }
    }

    public void DecrementHairCountdown()
    {
        hairCountdown = hairCountdown - 1;
    }

    public bool WantsHaircut()
    {
        return hairCountdown <= 0;
    }

    public void ReceiveHaircut()
    {
        hairCountdown = HaircutInterval;
    }
}
