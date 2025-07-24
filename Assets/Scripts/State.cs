using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    //the state can decide on the following tax rates
    bool inteheritanceTaxIsProgressive = true;
    float inheritanceTax = 20f;
    float addedValueTaxAlchohol = 40f;
    float addedValueTaxFood = 24f;
    float companyTax = 20f;
    bool incomeTaxIsProgressive = true;
    float incomeTax = 20f;

    //the state pays following
    float socialsupport = 20f;
    float healthcare = 20f;
}
