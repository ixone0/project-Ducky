using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowgameover : MonoBehaviour
{
    public Scene3System scene3system;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            scene3system.GameOver = true;
        }
    }

}
