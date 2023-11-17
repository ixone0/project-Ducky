using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptStartGame : MonoBehaviour
{
    public static bool StartGame;
    public GameObject SoundStart;

    void Start()
    {
        StartGame = false;
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.G))
        {
            SoundStart.SetActive(true);
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            StartGame = true;
        }
        
    }
}
