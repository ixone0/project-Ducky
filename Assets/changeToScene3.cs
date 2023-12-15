using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeToScene3 : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage3");
    }
}
