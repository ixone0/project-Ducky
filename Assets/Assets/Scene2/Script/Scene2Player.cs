using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Player : MonoBehaviour
{

    FirstPersonController firstPersonController;
    [SerializeField] GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = Player.GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        firstPersonController.canCrouch = false;
    }
}
