using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3System : MonoBehaviour
{
    [Header("Victory")]
    public bool Scene3Complete;
    public bool GameVictory;

    /*[Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorSpot;*/

    [Header("GameOver")]
    public GameObject UIGAMEOVER;
    public bool GameOver;
    private bool GameOver1;
    public GameObject JumpScare;

    void Start()
    {
        Scene3Complete = false;
        GameOver = false;
        GameOver1 = false;
        GameVictory = false;
    }

    void Update()
    {
        System();
    }
    
    void System()
    {
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
        if(!GameOver)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
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
