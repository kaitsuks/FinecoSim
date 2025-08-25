using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all companies (Hairdresser, Hotel, Restaurant, etc.)
/// Handles employees, wages, revenue, and tax payments.
/// </summary>
public class Company : MonoBehaviour
{
    // --- Basic company data ---
    public string companyName = "Generic Company";   // Company name
    public float basePrice = 50f;                    // Price before VAT
    public float reputation = 0.5f;                  // 0.0 bad → 1.0 excellent
    public int employees = 1;                        // Number of workers
    public float wagesPerEmployee = 2000f;           // Monthly salary

    // --- Economy tracking ---
    public float totalRevenue = 0f;                  // All sales
    public float totalWagesPaid = 0f;                // All wages paid
    public float totalTaxesPaid = 0f;                // All taxes paid

    // Reference to the State (government)
    public State government;

    /// <summary>
    /// Calculate final price including VAT (depends on sector).
    /// Example: hairdresser vs. hotel vs. food.
    /// </summary>
    public virtual float GetPriceWithVAT(float vatRate)
    {
        return basePrice * (1f + vatRate / 100f);
    }

    /// <summary>
    /// Simulate a customer purchase at this company.
    /// </summary>
    public virtual void ServeCustomer(Person customer, float vatRate)
    {
        float price = GetPriceWithVAT(vatRate);

        if (customer.money >= price)
        {
            // Customer pays
            customer.money -= price;
            totalRevenue += price;

            // VAT portion goes to government
            float vatAmount = price - basePrice;
            government.CollectTax(vatAmount);
            totalTaxesPaid += vatAmount;
        }
        else
        {
            // Customer cannot afford
            // Later: could record dissatisfaction
        }
    }

    /// <summary>
    /// Pay monthly wages to employees.
    /// </summary>
    public virtual void PayWages()
    {
        float wages = employees * wagesPerEmployee;

        if (totalRevenue >= wages)
        {
            totalRevenue -= wages;
            totalWagesPaid += wages;
            // In a full model: distribute wages to Person employees
        }
        else
        {
            // Not enough money to pay wages
            // Could lead to layoffs or bankruptcy
        }
    }
}
