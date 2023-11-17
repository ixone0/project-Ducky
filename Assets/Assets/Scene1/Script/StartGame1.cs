using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame1 : MonoBehaviour
{
    public GameObject timelineGhost;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            timelineGhost.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
