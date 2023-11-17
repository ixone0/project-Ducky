using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorduck : MonoBehaviour
{
    public GameObject doorduckfirst;
    public GameObject doorducksecond;

    public AudioSource SoundOpenDoor;
    public AudioSource SoundCloseDoor;

    void Start()
    {
        doorduckfirst.SetActive(true);
        doorducksecond.SetActive(false);
    }

    void Update()
    {
        if(DoorOpenClose.DoorDuckisOpen)
        {
            SoundCloseDoor.Play();
            doorduckfirst.SetActive(false);
            doorducksecond.SetActive(true);
        }
        if(!DoorOpenClose.DoorDuckisOpen)
        {
            SoundOpenDoor.Play();
            doorduckfirst.SetActive(true);
            doorducksecond.SetActive(false);
        }
    }
}
