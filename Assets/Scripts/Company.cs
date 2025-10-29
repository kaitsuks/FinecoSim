using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public string Name { get; set; }
    public List<Person> Employees { get; set; } = new List<Person>();

    public int weeklyCapacity = 50;      // can be set in inspector
    private int remainingCapacity;
    public float WeeklyRevenue { get; private set; }

    public void ResetWeek()
    {
        remainingCapacity = weeklyCapacity;
        WeeklyRevenue = 0f;
    }

    public void AddEmployee(Person employee)
    {
        if (Employees == null) Employees = new List<Person>();
        Employees.Add(employee);
        employee.WorksAtSalon = true;
    }

    // Try to serve a customer, returns true if served
    public bool TryServeCustomer(Person p, float price, float vat, State state)
    {
        if (remainingCapacity <= 0) return false;
        float totalPaid = price * (1f + vat);
        WeeklyRevenue += totalPaid;
        remainingCapacity--;

        if (state != null)
        {
            float vatAmount = price * vat;
            state.CollectTax(vatAmount);
        }

        p.Money -= totalPaid; // customer pays
        return true;
    }

    public int GetRemainingCapacity() => remainingCapacity;
}
