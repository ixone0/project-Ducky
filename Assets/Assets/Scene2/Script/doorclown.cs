using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorclown : MonoBehaviour
{
    public GameObject doorclownfirst;
    public GameObject doorclownsecond;

    public AudioSource SoundOpenDoor;
    public AudioSource SoundCloseDoor;

    void Start()
    {
        doorclownfirst.SetActive(true);
        doorclownsecond.SetActive(false);
    }


    void Update()
    {
        if(DoorOpenClose.DoorClownisOpen)
        {
            SoundCloseDoor.Play();
            doorclownfirst.SetActive(false);
            doorclownsecond.SetActive(true);
        }
        if(!DoorOpenClose.DoorClownisOpen)
        {
            SoundOpenDoor.Play();
            doorclownfirst.SetActive(true);
            doorclownsecond.SetActive(false);
        }
    }
}
