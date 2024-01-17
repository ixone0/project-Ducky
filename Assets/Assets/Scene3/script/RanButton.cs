using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RanButton : MonoBehaviour
{
    public static int NumberStage;
    private Scene3System scene3system;
    private FirstPersonController firstpersoncontroller;
    public GameObject uiPanel;          // Reference to the UI panel to show/hide.
    public GameObject UIpressE;
    public List<Button> buttons;        // List of UI buttons (numbered 1-9).
    private int currentButtonIndex = 0; // The current index to track the player's progress in the sequence.
    public bool lightsActive = false;  // Indicates whether the buttons are currently lighting up.
    public bool EqualSequences = false;
    public bool PlayerDetected;
    public float checkRadius = 3f;
    public LayerMask playerLayer;
    public bool playerWasDetected = false;
    public bool playerStillDetected = false;
    private Collider[] previousColliders;

    // Store the original colors of the buttons
    private List<Color> originalButtonColors;

    private List<int> correctSequence; // Store the correct sequence of lit buttons.
    private List<int> playerClicks;    // Store the player's clicked buttons.
    int buttonIndex;

    private bool correctInputProcessed = false;

    public AudioSource SoundEnter;
    public AudioSource SoundClick;

    void Start()
    {
        NumberStage = 0;
        previousColliders = new Collider[0];
        PlayerDetected = false;
        firstpersoncontroller = GameObject.Find("Player2").GetComponent<FirstPersonController>();
        scene3system = GameObject.Find("SystemScene3").GetComponent<Scene3System>(); 
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

    void Update()
    {
        if(!correctInputProcessed)
        {
            OverLap();
        }
        if(Input.GetKeyDown(KeyCode.E) && PlayerDetected && !correctInputProcessed) EnterPanel();
    }

    void OverLap()
    {
        Vector3 sphereCenter = transform.position;

        Collider[] currentColliders = Physics.OverlapSphere(sphereCenter, checkRadius, playerLayer);
        foreach (Collider currentCollider in currentColliders)
        {
            playerWasDetected = false;

            foreach (Collider previousCollider in previousColliders)
            {
                if (previousCollider == currentCollider)
                {
                    playerWasDetected = true;
                    break;
                }
            }

            if (!playerWasDetected)
            {
                Debug.Log("Player entered the sphere!");
                PlayerDetected = true;
                UIpressE.SetActive(true);
            }
        }

     
        foreach (Collider previousCollider in previousColliders)
        {
            playerStillDetected = false;

            foreach (Collider currentCollider in currentColliders)
            {
                if (previousCollider == currentCollider)
                {
                    playerStillDetected = true;
                    break;
                }
            }
           
            if (!playerStillDetected)
            {
                Debug.Log("Player exited the sphere!");
                PlayerDetected = false;
                UIpressE.SetActive(false);
            }
        }
        previousColliders = currentColliders;
    }

    void EnterPanel()
    {
        SoundEnter.Play();
        firstpersoncontroller.StopMovement();
        Debug.Log("Press E");
        UIpressE.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        uiPanel.SetActive(true); // When the player enters the "GAMEPLAY" area, the UI panel is shown.
        if (!lightsActive)
        {
            currentButtonIndex = 0;     // Reset the index for the player's progress.
            StartCoroutine(LightUpButton()); // Start lighting up buttons.
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
                    buttons[randomIndex].image.color = Color.black;
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
        yield return new WaitForSeconds(0.1f);

        if (!AreSequencesEqual())
        {
            StartCoroutine(WrongButtonEffect());
            Debug.Log("Wrong sequence!");
        }
        else if(AreSequencesEqual() && EqualSequences && !correctInputProcessed)
        {
            CorrectInput();
            Debug.Log("Correct sequence!");
        }
    }

    bool AreSequencesEqual()
    {
        if(playerClicks.Count == correctSequence.Count)
        {
            EqualSequences = true;
        }
        if(playerClicks.Count < correctSequence.Count)
        {
            EqualSequences = false;
        }
        
        for (int i = 0; i < playerClicks.Count; i++)
        {
            Debug.Log(correctSequence[i] + "-" + playerClicks[i]);
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
            SoundClick.Play();
            Debug.Log(buttonIndex);
            playerClicks.Add(buttonIndex);
           // Debug.Log(playerClicks.Count +  " - " + correctSequence.Count);

            buttons[buttonIndex].image.color = Color.green;
            StartCoroutine(ResetButtonColor(buttonIndex, 0.35f));
            StartCoroutine(CheckPlayerInput());
            lightsActive = false;
        }
    }

    IEnumerator ResetButtonColor(int buttonIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        buttons[buttonIndex].image.color = Color.black;
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
        scene3system.Gameplay[scene3system.i] = true;
        firstpersoncontroller.ContinueMovement();
        lightsActive = false;
        correctInputProcessed = true;
        uiPanel.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        scene3system.i += 1;
        ResetGame();
    }
}
