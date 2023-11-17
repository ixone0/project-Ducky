using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressLeftShift : MonoBehaviour
{   
    public GameObject panelToClose;

    void Update()
    {
        // เมื่อกดปุ่มใดปุ่มนึงใน keyboard (ตัวอย่างเช่น Space)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // ปิด UI object ที่กำหนด
            if (panelToClose != null)
            {
                panelToClose.SetActive(false);
            }

        }
    }
}
