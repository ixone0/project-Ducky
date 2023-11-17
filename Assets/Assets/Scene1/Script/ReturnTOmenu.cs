using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnTOmenu : MonoBehaviour
{
    public void ToScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("to");
    }
}
