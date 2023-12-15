using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene3Player : MonoBehaviour
{
    FirstPersonController firstPersonController;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = gameObject.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        firstPersonController.Scene3 = true;
        firstPersonController.lowerLookLimit = 60;
    }
}
