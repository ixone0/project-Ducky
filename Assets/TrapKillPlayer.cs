using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapKillPlayer : MonoBehaviour
{
    public GameObject JumpScare;
    public bool GameOver;

    void Start()
    {
        GameOver = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(!GameOver)
            {
                StartCoroutine(GAMEOVER());
            }
        }
    }

    IEnumerator GAMEOVER()
    {
        JumpScare.SetActive(true);
        Cursor.visible = false;
        GameOver = true;
        yield return new WaitForSeconds(1.8f);

        Debug.Log("GameOver");
        Time.timeScale = 0f;
        Debug.Log("Time scale set to 0");
    }
}
