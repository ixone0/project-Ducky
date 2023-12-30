using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3System : MonoBehaviour
{
    [Header("Victory")]
    public bool Scene3Complete;
    public bool GameVictory;

    [Header("Gameplay bool")]
    public bool[] Gameplay;

    /*[Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorSpot;*/

    [Header("GameOver")]
    public GameObject UIGAMEOVER;
    public bool GameOver;
    private bool GameOver1;
    public GameObject JumpScare;

    [Header("Gameplay")]
    public GameObject[] TimelineGameplay;
    public int i;

    void Start()
    {
        Gameplay = new bool[6];
        Scene3Complete = false;
        GameOver = false;
        GameOver1 = false;
        GameVictory = false;
    }

    void Update()
    {
        System();
        SystemGameplay();
    }
    
    void System()
    {
        if(Gameplay[0] && Gameplay[1] && Gameplay[2] && Gameplay[3] && Gameplay[4] && Gameplay[5])
        {
            GameVictory = true;
        }
        if(GameVictory)
        {

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

    void SystemGameplay()
    {
        if(Gameplay[0])
        {
            TimelineGameplay[0].SetActive(true);
        }
        if(Gameplay[1])
        {
            TimelineGameplay[1].SetActive(true);
        }
        if(Gameplay[2])
        {
            TimelineGameplay[2].SetActive(true);
        }
        if(Gameplay[3])
        {
            TimelineGameplay[3].SetActive(true);
        }
        if(Gameplay[4])
        {
            TimelineGameplay[4].SetActive(true);
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
    }


}
