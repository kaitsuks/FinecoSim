using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Person : MonoBehaviour
{
    //defining traits
    string gender = "male";
    int age = 20;
    float wage = 2000f;
    string savingsAccount = "12345678";
    int dayOfWage = 2;
    string occupation = "hardresser";
    string education = "master's degree";

    //consumption habits
    //alcohol
    string consumptionHabitsAlcoholPlace = "none"; // "home" "restaurant"
    string consumptionHabitsAlcohonAmmount = "none";
    string consumptionHabitsAlcohonAmmountChange = "none"; //"reducation", "no", "increase"

    //icecream
    string consumptionHabitsIcecream = "low";
    
    //lunch
    string consumptionHabitsLunchPlace = "home"; // "restaurant", "school"
    
    //dinner
    string consumptionHabitsDinnerPlace = "home"; // "restaurant", "school"

    //barbeber
    string consumptionHabitBarber = "does not like hair";

    //cinema, movies, festivals, travel
    string consumptionHabitsCinema = "None";
    string consumptionHabitsFestivals = "None";
    string consumptionHabitsTravel = "None";

    // person statuses
    float weightPerson = 80f;
    float heightPerson = 180f;
    string personality = "funny";
    string skincolour = "white";
    string hairType = "short";
    float hairGrowth = 1f;  
}