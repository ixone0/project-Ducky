using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapKillPlayer : MonoBehaviour
{
    public GameObject System;
    Scene3System scene3system;
    void Start()
    {
        System = GameObject.Find("SystemScene3");
        scene3system = System.GetComponent<Scene3System>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            scene3system.GameOver = true;
        }
    }
}
