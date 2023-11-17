using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressS : MonoBehaviour
{   
    public GameObject panelToClose;
    public GameObject panelToShow;

    void Update()
    {
        // เมื่อกดปุ่มใดปุ่มนึงใน keyboard (ตัวอย่างเช่น Space)
        if (Input.GetKeyDown(KeyCode.S))
        {
            // ปิด UI object ที่กำหนด
            if (panelToClose != null)
            {
                panelToClose.SetActive(false);
            }

            // แสดง UI object ที่กำหนด
            if (panelToShow != null)
            {
                panelToShow.SetActive(true);
            }
        }
    }
}
