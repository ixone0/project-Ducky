using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayScene3 : MonoBehaviour
{
    [SerializeField] private float checkRadius = 3;
    [SerializeField] private LayerMask ButtonLayer;

    public int i;
    public bool ClearButton;
    public bool IsAttachButton;
    public int[] RanNum = new int[10]; 

    void Start()
    {
        for (int j = 0; j < 4; j++)
        {
            RanNum[j] = 0;
        }
    }

    void Update()
    {
        Debug.Log("i: " + i);
        SystemButton();
    }

    void OnTriggerEnter(Collider b)
    {
        if (b.gameObject.CompareTag("Button"))
        {
            Debug.Log("Hit");
            i++;
            int randomValue = Random.Range(1, 10);
            RanNum[i - 1] = randomValue;
            b.tag = "ButtonPhase2";

            if (i == 1)
            {
                Debug.Log(RanNum[i - 1]);
            }
            if (i == 2)
            {
                Debug.Log(RanNum[i - 2] + " " + RanNum[i - 1]);
            }
        }
    }

    void SystemButton()
    {
        GameObject[] Buttons = GameObject.FindGameObjectsWithTag("Button");
        if (Buttons.Length == 0)
        {
            Debug.Log("Clear all Buttons!");
        }
    }
}
