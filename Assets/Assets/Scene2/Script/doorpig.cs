using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorpig : MonoBehaviour
{
    public GameObject doorpigfirst;
    public GameObject doorpigsecond;

    public AudioSource SoundOpenDoor;
    public AudioSource SoundCloseDoor;

    void Start()
    {
        doorpigfirst.SetActive(true);
        doorpigsecond.SetActive(false);
    }


    void Update()
    {
        if(DoorOpenClose.DoorPigisOpen)
        {
            SoundCloseDoor.Play();
            doorpigfirst.SetActive(false);
            doorpigsecond.SetActive(true);
        }
        if(!DoorOpenClose.DoorPigisOpen)
        {
            SoundOpenDoor.Play();
            doorpigfirst.SetActive(true);
            doorpigsecond.SetActive(false);
        }
    }
}
