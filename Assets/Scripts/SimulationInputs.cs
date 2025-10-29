using UnityEngine;
using TMPro;

public class SimulationInputs : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField priceInput;
    public TMP_InputField vatInput;
    public TMP_InputField populationInput;
    public TMP_InputField hairdressersInput;
    public TMP_InputField customersPerHairdresserInput;
    public TMP_InputField intervalInput;

    [Header("References")]
    public SimulationController simulationController;

    public void ApplyInputs()
    {
        if (simulationController == null)
        {
            Debug.LogError("SimulationController not linked!");
            return;
        }

        float price = ParseFloat(priceInput.text, 20f);
        float vat = ParseFloat(vatInput.text, 0.20f);
        int population = ParseInt(populationInput.text, 1000);
        int hairdressers = ParseInt(hairdressersInput.text, 20);
        int customersPer = ParseInt(customersPerHairdresserInput.text, 50);
        float interval = ParseFloat(intervalInput.text, 0.5f);

        simulationController.InitializeSimulation(price, vat, population, hairdressers, customersPer, interval);
    }

    private float ParseFloat(string text, float defaultValue)
    {
        return float.TryParse(text, out float val) ? val : defaultValue;
    }

    private int ParseInt(string text, int defaultValue)
    {
        return int.TryParse(text, out int val) ? val : defaultValue;
    }
}
