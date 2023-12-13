using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanButton : MonoBehaviour
{
    public GameObject uiPanel;          // Reference to the UI panel to show/hide.
    public List<Button> buttons;        // List of UI buttons (numbered 1-9).
    private List<Button> litButtons;    // List to store the currently lit buttons.
    private int currentButtonIndex = 0; // The current index to track the player's progress in the sequence.
    private bool lightsActive = false;  // Indicates whether the buttons are currently lighting up.

    // Store the original colors of the buttons
    private List<Color> originalButtonColors;

    private List<int> correctSequence; // Store the correct sequence of lit buttons.
    private List<int> playerClicks;    // Store the player's clicked buttons.

    void Start()
    {
        uiPanel.SetActive(false); // Initially, the UI panel is hidden.
        litButtons = new List<Button>(); // Initialize the list to store lit buttons.
        originalButtonColors = new List<Color>(); // Initialize the list to store the original button colors.
        correctSequence = new List<int>(); // Initialize the list to store the correct sequence.
        playerClicks = new List<int>();

        // Store the original colors of the buttons
        foreach (Button button in buttons)
        {
            originalButtonColors.Add(button.image.color);
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            uiPanel.SetActive(true); // When the player enters the "GAMEPLAY" area, the UI panel is shown.
            if (!lightsActive)
            {
                litButtons.Clear();          // Clear the list of lit buttons.
                currentButtonIndex = 0;     // Reset the index for the player's progress.
                StartCoroutine(LightUpButton()); // Start lighting up buttons.
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
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

        // Disable button interaction during animation
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        // Generate a random sequence of buttons to light up
        for (int i = 0; i < 4; i++)
        {
            if (buttons.Count > 0)
            {
                int randomIndex = Random.Range(0, buttons.Count);
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

        // Re-enable button interaction after animation
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        lightsActive = false;

        // Check if the player's clicks match the correct sequence
        StartCoroutine(CheckPlayerInput());
    }

    IEnumerator CheckPlayerInput()
    {
        if (AreSequencesEqual() == true)
        {
            Debug.Log("Correct sequence!");
        }
        else
        {
            StartCoroutine(WrongButtonEffect());
        }

        yield return null;
    }

    bool AreSequencesEqual()
    {
        if (correctSequence.Count != playerClicks.Count)
        {
            return false;
        }

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (correctSequence[i] != playerClicks[i])
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator OnButtonClicked(Button clickedButton)
    {
        if (lightsActive)
        {
            int buttonIndex = buttons.IndexOf(clickedButton);
            playerClicks.Add(buttonIndex);

            buttons[buttonIndex].image.color = Color.green;
            yield return new WaitForSeconds(1.0f); // Light up each button for 1 second.

            // Check if the player's clicks match the correct sequence
            StartCoroutine(CheckPlayerInput());
        }
    }

    IEnumerator WrongButtonEffect()
    {
        // Check if the player has made at least one click
        if (playerClicks.Count > 0)
        {
            foreach (Button button in buttons)
            {
                button.image.color = Color.red; // Turn the lit buttons red.
            }

            yield return new WaitForSeconds(1.0f); // Wait for a specified duration (e.g., 1 second).

            // Restore the original colors of the buttons
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].image.color = originalButtonColors[i];
            }
        }

        buttons.Clear();    // Clear the list of lit buttons.
        currentButtonIndex = 0; // Reset the player's progress.
        correctSequence.Clear();   // Clear the correct sequence.
        playerClicks.Clear();      // Clear the player's clicks.
    }
}
