using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public string text;
    public void ToScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(text);
    }
}
