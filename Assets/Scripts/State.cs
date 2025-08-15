using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class State : MonoBehaviour
{
    //the state can decide on the following tax rates
    //added value tax
    float addedValueTaxHairdresser = 24f;
    float addedValueTaxHotel = 24f;
    float addedValueTaxFood = 24f;
    float addedValueTaxAlchohol = 24f;
    float addedValueTaxTobacco = 24f;
    float addedValueTaxSugar = 0f;
    float addedValueTaxServiceIndustry = 24f;
    float addedValueTaxLuxury = 24f;
    float addedValueTaxCleaningService = 24f;

    //wages
    float wageBarber = 2000f;
    float wageHotelWorker = 2000f;
    float wageHotelBoss = 2000f;

    //inheritance tax
    bool inteheritanceTaxIsProgressive = true;
    float inheritanceTax = 20f;

    //company tax
    bool incomeTaxIsProgressive = true;
    float companyTax = 20f;

    //social support
    //float toimeentulotuki = 20f; // ammattiliitot maksavat(?)
    float peruspäiväraha = 20f; // hallitus maksaa jos ei ole ammattiliitossa mukana (?)
    float työttömyyspäiväraha = 20f;
    float HouseLivingSupport = 20f; //asuntotuki, bostadsstöd  

    //social spending
    float hospitals = 20f; //hyvinvointialue(?)
    float elderlyCare = 20f; //hyvinvointialue(?)
    float treatmentAlcholism = 20f; //hyvinvointialue(?)

    float loans = 1f;

    float governmentApprovalPercent = 50f;

    float "marginaalivero" = 1f;
}