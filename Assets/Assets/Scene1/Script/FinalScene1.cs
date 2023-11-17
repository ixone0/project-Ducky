using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene1 : MonoBehaviour
{
    public GameObject timelineVictoryScene1;

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            timelineVictoryScene1.SetActive(true);
        }
    }
}
