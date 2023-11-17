using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2Scp : MonoBehaviour
{
    
    public GameObject DoorFirst;
    public GameObject DoorSecond;
    public bool DoorIsClose = true; 

    void Start()
    {
        UpdateDoorState(); // Set the initial door state
    }

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
        }
        // No need to check DoorIsClose in Update; it's better to respond to the OnTriggerStay event
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E)) // Use GetKeyDown to trigger only once per press
            {
                DoorIsClose = !DoorIsClose; // Toggle the door state
                UpdateDoorState(); // Apply the new door state
            }

        }
    }

    void UpdateDoorState()
    {
        DoorFirst.SetActive(DoorIsClose);
        DoorSecond.SetActive(!DoorIsClose);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    /*
    void Start()
    {
        DoorFirst2.SetActive(true);
        DoorSecond2.SetActive(false);
        DoorIsClose = true;
    }

    void Update() 
    {
        if(DoorIsClose)
        {
            DoorFirst2.SetActive(true);
            DoorSecond2.SetActive(false);
        }
        if(DoorIsClose = false)
        {
            DoorFirst2.SetActive(false);
            DoorSecond2.SetActive(true);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("E");
            if(Input.GetKey(KeyCode.E))
            {
                if(DoorIsClose)
                {
                    DoorIsClose = false;
                }
                if(!DoorIsClose)
                {
                    DoorIsClose = true;
                }
            }
        }
    }
    */
}


/*
        if(DoorIsClose)
        {
            DoorFirst2.SetActive(true);
            DoorSecond2.SetActive(false);
        }
        if(!DoorIsClose)
        {
            DoorFirst2.SetActive(false);
            DoorSecond2.SetActive(true);
        }
*/