using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene1System : MonoBehaviour
{
    [Header("Timer")]
    public float time;
    public bool TimeStart;
    [SerializeField] TextMeshProUGUI timerText; 

    [Header("Component")]
    public bool Button1;
    public bool Button2;
    public bool Button3;
    public bool Button4;
    public bool Button5;

    [Header("UI")]
    public GameObject UIGAMEOVER;
    public static bool GameOver;
    public bool Scene1Complete;
    int minutes;
    int seconds;

    [Header("Player")]
    [SerializeField] GameObject Player;
    FirstPersonController firstPersonController;

    [Header("GameOver")]
    public GameObject JumpScare;
    public bool GameOver1;

    [Header("Victory")]
    public GameObject timelineVictory;
    public GameObject timelineVictoryScene1;
    public static bool GameVictory;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorSpot;

   

    void Start()
    {
        firstPersonController = Player.GetComponent<FirstPersonController>();
        TimeStart = true;  
        Button1 = false;
        Button2 = false;
        Button3 = false;
        Button4 = false;
        Button5 = false;
        GameOver = false;
        Scene1Complete = false;
        time = 300f;
        GameOver1 = false;
        GameVictory = false;

        cursorSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorSpot, CursorMode.Auto);
    }

    void Update()
    {     
        Timer();
        SystemScene1();
        
    }


    void Timer()
    {

        if(TimeStart)
        {
            timerText.enabled = true;
            time -= Time.deltaTime;
            minutes = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);
        }
        if(time <= 0)
        {
            time = 0;
            GameOver = true;
            TimeStart = false;
            minutes = 0;
            seconds = 0;
            firstPersonController.enabled = false;
        }
        if(!GameOver)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void SystemScene1()
    {

        if(Button1 && Button2 && Button3 && Button4 && Button5)
        {
            Debug.Log("ButtonFinish");
            timelineVictory.SetActive(true);
        }
        if(GameVictory)
        {
            Debug.Log("VictoryScene1");
            timelineVictoryScene1.SetActive(true);
        }

        if(GameOver)
        {
            StartCoroutine(GAMEOVER());
        }
        if(GameOver1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    IEnumerator GAMEOVER()
    {
        JumpScare.SetActive(true);
        Cursor.visible = false;
        yield return new WaitForSeconds(1.8f);

        Debug.Log("GameOver");
        GameOver1 = true;
        Time.timeScale = 0f;
        Debug.Log("Time scale set to 0");
    }

    
}
