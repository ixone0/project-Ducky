using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCursor : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
