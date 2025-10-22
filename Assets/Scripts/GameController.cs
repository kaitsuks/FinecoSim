using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startMenuPanel;
    public GameObject simulationButtonPanel;
    public GameObject graphPanel;

    [Header("References")]
    public Button simulationButton;
    public SimulationController simulationController;

    private bool startMenuPassed = false;
    private bool simulationStarted = false;

    void Start()
    {
        // Show only the StartMenu at the beginning
        startMenuPanel.SetActive(true);
        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(false);

        // Connect the button press to StartSimulation()
        simulationButton.onClick.AddListener(StartSimulation);
    }

    void Update()
    {
        if (!startMenuPassed) // start menu
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("1. The start menu has been passed with ENTER");
                startMenuPassed = true;
                startMenuPanel.SetActive(false);
                simulationButtonPanel.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("2. Cancelled from the start menu with ESC");
                Application.Quit();
            }
        }
        else if (!simulationStarted) // waiting at simulation button
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("4. Cancelled at the simulation button panel with ESC");
                Application.Quit();
            }
        }
        else if (simulationStarted) // simulation running
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("6. Cancelled during simulation with ESC");
                Application.Quit();
            }
        }
    }

    public void StartSimulation() // this has to be "public" otherwise it cannot be added in "OnClick()"
    {
        Debug.Log("3. The simulation button has been pressed");
        simulationStarted = true;

        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(true);

        // Starting the simulation logic here
        if (simulationController != null)
        {
            simulationController.StartSimulation();
            Debug.Log("5. The simulation has started (SimulationController running)");
        }
        else
        {
            Debug.LogError("No SimulationController is linked to GameController!");
        }
    }
}
