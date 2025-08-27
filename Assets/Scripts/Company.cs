using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base class for all companies.
/// Handles revenue, wages, and tax collection.
/// </summary>
public class Company : MonoBehaviour
{
    public string companyName = "Hairdresser";
    public float basePrice = 20f;
    public int employees = 1;
    public float wagesPerEmployee = 2000f;

    public float totalRevenue = 0f;
    public float totalWagesPaid = 0f;
    public float totalTaxesPaid = 0f;

    public State government;

    // Sell a service (like haircut) to a customer
    public virtual void ServeCustomer(Person customer, float vatRate)
    {
        float price = basePrice * (1f + vatRate);

        if (customer.Money >= price)
        {
            customer.Money -= price;
            totalRevenue += price;

            float vatAmount = basePrice * vatRate;
            government.CollectTax(vatAmount);
            totalTaxesPaid += vatAmount;
        }
    }

    // Pay monthly wages to employees (simplified: evenly to all citizens)
    public virtual void PayWages(List<Person> allPeople)
    {
        float wages = employees * wagesPerEmployee;

        if (totalRevenue >= wages && allPeople.Count > 0)
        {
            totalRevenue -= wages;
            totalWagesPaid += wages;

            float perPerson = wages / allPeople.Count;
            foreach (var p in allPeople)
                p.Money += perPerson;
        }
    }
}
