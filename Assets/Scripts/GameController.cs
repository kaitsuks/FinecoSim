using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startMenuPanel;
    public GameObject simulationButtonPanel;
    public GameObject graphPanel;

    [Header("References")]
    public Button simulationButton; // the button in SimulationButtonPanel

    private bool startMenuPassed = false;
    private bool simulationStarted = false;

    void Start()
    {
        // Show only the StartMenu in the beginning
        startMenuPanel.SetActive(true);
        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(false);

        // Connecting the function of the button
        simulationButton.onClick.AddListener(StartSimulation);
    }

    void Update()
    {
        if (!startMenuPassed)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // Enter
            {
                startMenuPassed = true;
                startMenuPanel.SetActive(false);
                simulationButtonPanel.SetActive(true); // show the button
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        else if (simulationStarted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private void StartSimulation()
    {
        simulationStarted = true;
        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(true); // Showing the graph + text
    }
}
