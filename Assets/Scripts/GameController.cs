using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject startMenuPanel;
    public GameObject simulationButtonPanel;
    public GameObject graphPanel;
    public GameObject previousScreenPanel;

    [Header("References")]
    public Button simulationButton;
    public Button previousScreen;
    public SimulationController simulationController;

    private bool startMenuPassed = false;
    private bool simulationStarted = false;
    public AudioSource SimulationButtonPanelAudio;
    public AudioSource GraphPanelAudio;

    void Start()
    {
        // Showing only the StartMenu at the beginning
        startMenuPanel.SetActive(true);
        simulationButtonPanel.SetActive(false);
        graphPanel.SetActive(false);
        previousScreenPanel.SetActive(false);

        // Connecting the button presses
        simulationButton.onClick.AddListener(StartSimulation);
        previousScreen.onClick.AddListener(BackToSimulationButtonPanel);
    }

    void Update() // Checking if the user presses ESC
    {
        if (!startMenuPassed) // Start Menu
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Start menu passed with ENTER");
                startMenuPassed = true;
                startMenuPanel.SetActive(false);
                simulationButtonPanel.SetActive(true);
                PlaySimulationButtonPanelAudio();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Cancelled from the start menu with ESC");
                Application.Quit();
            }
        }
        else if (!simulationStarted) // Simulation Button Panel
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Cancelled at the simulation button panel with ESC");
                Application.Quit();
            }
        }
        else if (simulationStarted) // Simulation Running
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Cancelled during simulation with ESC");
                Application.Quit();
            }
        }
    }

    // Function to start the simulation and move to GraphPanel
    public void StartSimulation()
    {
        Debug.Log("Simulation button has been pressed");
        simulationStarted = true;

        // Stop the music for SimulationButtonPanel if it is playing
        if (SimulationButtonPanelAudio.isPlaying)
        {
            SimulationButtonPanelAudio.Stop();
        }

        // Hide SimulationButtonPanel and show GraphPanel with PreviousScreenPanel
        simulationButtonPanel.SetActive(false);
        previousScreenPanel.SetActive(true); // Show the PreviousScreenPanel when going to GraphPanel
        graphPanel.SetActive(true);

        PlayGraphPanelAudio();

        if (simulationController != null)
        {
            simulationController.StartSimulation();
            Debug.Log("Simulation has started (SimulationController running)");
        }
        else
        {
            Debug.LogError("No SimulationController is linked to GameController!");
        }
    }

    // Function to go back to SimulationButtonPanel from GraphPanel/PreviousScreenPanel
    public void BackToSimulationButtonPanel()
    {
        if (simulationController != null)
        {
            simulationController.ResetGraph();
        }

        // Stop the music for GraphPanel when we go back
        if (GraphPanelAudio.isPlaying)
        {
            GraphPanelAudio.Stop();
        }

        Debug.Log("Going back to SimulationButtonPanel");

        // Hide GraphPanel and PreviousScreenPanel before showing SimulationButtonPanel
        graphPanel.SetActive(false);
        previousScreenPanel.SetActive(false); // Hide PreviousScreenPanel

        simulationButtonPanel.SetActive(true); // Show SimulationButtonPanel

        PlaySimulationButtonPanelAudio();
    }

    private void PlaySimulationButtonPanelAudio()
    {
        if (SimulationButtonPanelAudio != null && !SimulationButtonPanelAudio.isPlaying)
        {
            SimulationButtonPanelAudio.Play();
        }
    }

    // Function to play the audio for the GraphPanel
    private void PlayGraphPanelAudio()
    {
        if (GraphPanelAudio != null && !GraphPanelAudio.isPlaying)
        {
            GraphPanelAudio.Play();
        }
    }
}
