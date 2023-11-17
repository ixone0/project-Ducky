using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTopick : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Pickscene");
    }

}
