using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishOpen : MonoBehaviour
{
    public SDuckClimb sduckclimb;
    // Start is called before the first frame update
    void Start()
    {
        sduckclimb.speed = 1f;
    }
}
