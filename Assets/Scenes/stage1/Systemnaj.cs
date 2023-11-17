using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systemnaj : MonoBehaviour
{

    [Header("BinRoom")]
    public bool BinComplete1;
    public bool BinComplete2;
    public bool BinComplete3;
    
    [Header("Check")]
    public bool BinRoomComplete;

    [Header("Goal")]
    public bool Stage1Complete = false;

    public void Start() 
    {
        BinComplete1 = false;
        BinComplete2 = false;
        BinComplete3 = false;
        BinRoomComplete = false;
        Stage1Complete = false;
    }

    public void Update()
    {
        if(BinComplete1 && BinComplete2 && BinComplete3)
        {
            BinRoomComplete = true;
        }

        if(BinRoomComplete)
        {
            Stage1Complete = true;
            Debug.Log("Stage1Complete");
        }
    }

    
}