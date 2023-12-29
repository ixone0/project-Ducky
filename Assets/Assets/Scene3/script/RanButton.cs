using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanButton : MonoBehaviour
{
    private SystemScene3 systemscene3;
    private GameObject uiPanel;          // Reference to the UI panel to show/hide.
    public List<Button> buttons;        // List of UI buttons (numbered 1-9).
    private int currentButtonIndex = 0; // The current index to track the player's progress in the sequence.
    private bool lightsActive = false;  // Indicates whether the buttons are currently lighting up.

    // Store the original colors of the buttons
    private List<Color> originalButtonColors;

    private List<int> correctSequence; // Store the correct sequence of lit buttons.
    private List<int> playerClicks;    // Store the player's clicked buttons.
    int buttonIndex;

    void Start()
    {
        systemscene3 = GameObject.Find("Scene3Sytem").GetComponent<SystemScene3>(); 
        uiPanel = GameObject.Find("UIpanel");
        uiPanel.SetActive(false); // Initially, the UI panel is hidden.
        originalButtonColors = new List<Color>(); // Initialize the list to store the original button colors.
        correctSequence = new List<int>(); // Initialize the list to store the correct sequence.
        playerClicks = new List<int>();

        // Store the original colors of the buttons
        foreach (Button button in buttons)
        {
            int buttonIndex = buttons.IndexOf(button);
            originalButtonColors.Add(button.image.color);
            button.onClick.AddListener(() => { lightsActive = true; OnButtonClicked(buttonIndex); });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiPanel.SetActive(true); // When the player enters the "GAMEPLAY" area, the UI panel is shown.
            if (!lightsActive)
            {
                currentButtonIndex = 0;     // Reset the index for the player's progress.
                StartCoroutine(LightUpButton()); // Start lighting up buttons.
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiPanel.SetActive(false); // When the player exits the "GAMEPLAY" area, the UI panel is hidden.
            lightsActive = false;   // Lights are turned off.
        }
    }

    IEnumerator LightUpButton()
    {
        lightsActive = true;

        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        // Generate a random sequence of buttons to light up
        correctSequence.Clear(); // Clear the previous sequence
        playerClicks.Clear(); // Clear player clicks

        int randomIndex = 0;
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 5; i++)
        {
            if (buttons.Count > 0)
            {
                randomIndex = Random.Range(0, buttons.Count); // Remove the int declaration here
                correctSequence.Add(randomIndex);

                if (randomIndex >= 0 && randomIndex < buttons.Count)
                {
                    buttons[randomIndex].image.color = Color.green;
                    yield return new WaitForSeconds(1.0f); // Light up each button for 1 second.
                    buttons[randomIndex].image.color = originalButtonColors[randomIndex];
                    yield return new WaitForSeconds(0.5f); // Pause between button lights.
                }
            }   
        }

        lightsActive = false;

        // Re-enable button interaction after animation
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        Debug.Log("Correct Sequence: " + string.Join(", ", correctSequence));

        // Check if the player's clicks match the correct sequence

    }

    IEnumerator CheckPlayerInput()
    {
        yield return new WaitForSeconds(0.5f);

        if (!AreSequencesEqual())
        {
            StartCoroutine(WrongButtonEffect());
            Debug.Log("Wrong sequence!");
        }
        else
        {
            CorrectInput();
            Debug.Log("Correct sequence!");
        }
    }

    bool AreSequencesEqual()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (correctSequence[i] != playerClicks[i])
            {
                return false;
            }
        }
        return true;
    }

    void OnButtonClicked(int buttonIndex)
    {
        if (lightsActive)
        {
            Debug.Log(buttonIndex);
            playerClicks.Add(buttonIndex);
            Debug.Log(playerClicks);

            Debug.Log("Player Sequence: " + string.Join(", ", playerClicks));

            buttons[buttonIndex].image.color = Color.green;
            StartCoroutine(ResetButtonColor(buttonIndex, 0.5f));
            StartCoroutine(CheckPlayerInput());
        }
    }

    IEnumerator ResetButtonColor(int buttonIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        buttons[buttonIndex].image.color = originalButtonColors[buttonIndex];
    }

    IEnumerator WrongButtonEffect()
    {
        foreach (Button button in buttons)
        {
            button.image.color = Color.red; // Turn the lit buttons red.
        }

        yield return new WaitForSeconds(1.0f); // Wait for a specified duration (e.g., 1 second).

        // Reset the game state
        ResetGame();
        // Start a new sequence
        StartCoroutine(LightUpButton());
    }

    void ResetGame()
    {
        correctSequence.Clear();
        playerClicks.Clear();
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].image.color = originalButtonColors[i];
        }
    }

    void CorrectInput()
    {
        systemscene3.Gameplay[0] = true;
        uiPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(gameObject);
    }
}
