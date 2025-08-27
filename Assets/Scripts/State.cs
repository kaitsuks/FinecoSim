using UnityEngine;

/// <summary>
/// Represents the government / state.
/// Manages taxes, collects revenue, and pays social benefits.
/// </summary>
public class State : MonoBehaviour
{
    // VAT (percentages)
    public float vatHairdresser = 0.20f;
    public float vatHotel = 0.20f;
    public float vatFood = 0.14f;
    public float vatAlcohol = 0.24f;

    // Company & income tax
    public bool incomeTaxIsProgressive = true;
    public float companyTax = 0.20f;

    // Welfare
    public float unemploymentBenefit = 500f;
    public float housingSupport = 300f;

    // Public spending
    public float healthcareSpending = 1000f;
    public float elderlyCareSpending = 800f;

    // Economy tracking
    public float totalTaxesCollected = 0f;
    public float totalBenefitsPaid = 0f;

    public void CollectTax(float amount)
    {
        totalTaxesCollected += amount;
    }

    public void PayUnemployment(Person person)
    {
        person.Money += unemploymentBenefit;
        totalBenefitsPaid += unemploymentBenefit;
    }

    public float GetNetBudget()
    {
        return totalTaxesCollected - totalBenefitsPaid
               - healthcareSpending - elderlyCareSpending;
    }
}
