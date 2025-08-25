using UnityEngine;

/// <summary>
/// Represents the government / state.
/// Manages taxes, collects revenue, and pays social benefits.
/// </summary>
public class State : MonoBehaviour
{
    // --- Tax rates (percentages) ---
    public float vatHairdresser = 24f;
    public float vatHotel = 24f;
    public float vatFood = 14f;
    public float vatAlcohol = 24f;

    // --- Company & income tax ---
    public bool incomeTaxIsProgressive = true;
    public float companyTax = 20f;

    // --- Welfare / social support ---
    public float unemploymentBenefit = 500f;
    public float housingSupport = 300f;

    // --- Public spending ---
    public float healthcareSpending = 1000f;
    public float elderlyCareSpending = 800f;

    // --- Economy tracking ---
    public float totalTaxesCollected = 0f;
    public float totalBenefitsPaid = 0f;

    /// <summary>
    /// Collects tax revenue from companies or persons.
    /// </summary>
    public void CollectTax(float amount)
    {
        totalTaxesCollected += amount;
    }

    /// <summary>
    /// Pays unemployment support to a citizen.
    /// </summary>
    public void PayUnemployment(Person person)
    {
        person.money += unemploymentBenefit;
        totalBenefitsPaid += unemploymentBenefit;
    }

    /// <summary>
    /// End-of-month government budget balance.
    /// </summary>
    public float GetNetBudget()
    {
        return totalTaxesCollected - totalBenefitsPaid
               - healthcareSpending - elderlyCareSpending;
    }
}
