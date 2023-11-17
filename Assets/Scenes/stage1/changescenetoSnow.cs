using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescenetoSnow : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Snow 1");
    }
}