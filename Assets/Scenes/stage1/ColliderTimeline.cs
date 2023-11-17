using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTimeline : MonoBehaviour
{
    public GameObject timelineEnter;
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "StartTimelineEnter")
        {
            timelineEnter.SetActive(true);
        }
    }
}
