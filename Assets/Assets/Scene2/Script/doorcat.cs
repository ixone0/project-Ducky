using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorcat : MonoBehaviour
{
    public GameObject doorcatfirst;
    public GameObject doorcatsecond;

    public AudioSource SoundOpenDoor;
    public AudioSource SoundCloseDoor;

    void Start()
    {
        doorcatfirst.SetActive(true);
        doorcatsecond.SetActive(false);
    }


    void Update()
    {
        if(DoorOpenClose.DoorCatisOpen)
        {
            SoundCloseDoor.Play();
            doorcatfirst.SetActive(false);
            doorcatsecond.SetActive(true);
        }
        if(!DoorOpenClose.DoorCatisOpen)
        {
            SoundOpenDoor.Play();
            doorcatfirst.SetActive(true);
            doorcatsecond.SetActive(false);
        }
    }
}

