using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDuckClimb : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed as needed
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
    void Update()
    {
        // Move the object upward along the Y-axis
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
