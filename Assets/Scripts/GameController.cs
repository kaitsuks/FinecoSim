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

    private bool startMenuPassed = false;
    private bool simulationStarted = false;

    void Start()
    {
        // Showing only the StartMenu in the beginning
        startMenuPanel.SetActive(true);
        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(false);

        // connection the press of button to "StartSimulation()"
        simulationButton.onClick.AddListener(StartSimulation);
    }

    void Update()
    {
        if (!startMenuPassed) // start menu
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("1. the start menu has been passed with ENTER");
                startMenuPassed = true;
                startMenuPanel.SetActive(false);
                simulationButtonPanel.SetActive(true); // showing the button
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("2. One has cancelled from the start menu with ESQ");
                Application.Quit();
            }
        }
        else if (!simulationStarted) // the panel with the simulation button
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("4. One has cancelled in the panel with the simulation button with ESQ");
                Application.Quit();
            }
        }
        else if (simulationStarted) // the simulation is run
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("6. One has cancelled under simulation with ESC");
                Application.Quit();
            }
        }
    }

    private void StartSimulation()
    {
        Debug.Log("3. The simulation button has been pressed");
        simulationStarted = true;
        simulationButtonPanel.SetActive(false); // Removing the button
        graphPanel.SetActive(true); // Showing the graph and texts
        Debug.Log("5. The simulation has started");
    }
}
