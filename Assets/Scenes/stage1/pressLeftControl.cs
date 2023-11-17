using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pressLeftControl : MonoBehaviour
{   
    public GameObject panelToClose;
    public GameObject panelToShow;

    void Update()
    {
        // เมื่อกดปุ่มใดปุ่มนึงใน keyboard (ตัวอย่างเช่น Space)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // ปิด UI object ที่กำหนด
            if (panelToClose != null)
            {
                panelToClose.SetActive(false);
            }

            if (panelToShow != null)
            {
                panelToShow.SetActive(true);
            }
        }
    }
}
