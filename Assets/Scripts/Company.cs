public class Hairdresser : Company
{
    public int capacityPerWeek = 100; // hur många kunder en frisör hinner
    private int servedThisWeek = 0;

    public override void ServeCustomer(Person customer, float vatRate)
    {
        if (servedThisWeek >= capacityPerWeek) return; // fullbokat

        float price = basePrice * (1f + vatRate);
        if (customer.WantsHaircut(price))
        {
            customer.Money -= price;
            customer.GetHaircut();
            totalRevenue += price;

            float vatAmount = basePrice * vatRate;
            government.CollectTax(vatAmount);
            totalTaxesPaid += vatAmount;

            servedThisWeek++;
        }
    }

    public void ResetWeek()
    {
        servedThisWeek = 0;
    }
}
