using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScene3 : MonoBehaviour
{
    public GameObject uiPanel;          // Reference to the UI panel to show/hide.
    public List<Button> buttons;        // List of UI buttons (numbered 1-9).
    private List<Button> litButtons;    // List to store the currently lit buttons.
    private int currentButtonIndex = 0; // The current index to track the player's progress in the sequence.
    private bool lightsActive = false;  // Indicates whether the buttons are currently lighting up.

    // Store the original colors of the buttons
    private List<Color> originalButtonColors;

    void Start()
    {
        uiPanel.SetActive(false); // Initially, the UI panel is hidden.
        litButtons = new List<Button>(); // Initialize the list to store lit buttons.
        originalButtonColors = new List<Color>(); // Initialize the list to store the original button colors.

        // Store the original colors of the buttons
        foreach (Button button in buttons)
        {
            originalButtonColors.Add(button.image.color);
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
                LightRandomButtons();       // Start lighting up buttons.
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

    void LightRandomButtons()
    {
        // Restore the original colors of the buttons
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].image.color = originalButtonColors[i];
        }

        List<Button> randomButtons = new List<Button>(buttons);
        for (int i = 0; i < 4; i++)
        {
            if (randomButtons.Count == 0)
                break;

            int randomIndex = Random.Range(0, randomButtons.Count);
            litButtons.Add(randomButtons[randomIndex]);

            // Change the button's color to green
            litButtons[i].image.color = Color.green;

            randomButtons.RemoveAt(randomIndex);
        }

        lightsActive = true;
    }

    public void ButtonClick(int buttonNumber)
    {
        if (lightsActive && currentButtonIndex < litButtons.Count)
        {
            if (litButtons[currentButtonIndex].name == "Button" + buttonNumber)
            {
                currentButtonIndex++;

                if (currentButtonIndex == litButtons.Count)
                {
                    lightsActive = false;
                    StartCoroutine(ResetAndLightButtons());
                    Debug.Log("Success");
                }
            }
            else
            {
                currentButtonIndex = 0;
                StartCoroutine(WrongButtonEffect());
            }
        }
    }

    IEnumerator ResetAndLightButtons()
    {
        yield return new WaitForSeconds(1.0f); // Wait for a specified duration (e.g., 1 second).

        currentButtonIndex = 0; // Reset the player's progress in the sequence.
        LightRandomButtons();  // Start lighting up buttons again.
    }

    IEnumerator WrongButtonEffect()
    {
        foreach (Button button in litButtons)
        {
            button.image.color = Color.red; // Turn the lit buttons red.
        }

        yield return new WaitForSeconds(1.0f); // Wait for a specified duration (e.g., 1 second).

        // Restore the original colors of the buttons
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].image.color = originalButtonColors[i];
        }
        litButtons.Clear();    // Clear the list of lit buttons.
        currentButtonIndex = 0; // Reset the player's progress.
    }
}