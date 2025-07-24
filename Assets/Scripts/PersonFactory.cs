using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PersonFactory : MonoBehaviour
{
    //this class is to generate agents either by random or from pool
    //traits of Person-agents
    string gender = "male";
    int age = 20;
    string occupation = "barberer";
    float WillingnesToConsume = 100;
    string personality = "calmn"; 
    //income, at this phase still defined, later to be randomised and dependent of societal factors e.g. employment
    float tulotaso = 100f;
}
