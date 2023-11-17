using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBlink : MonoBehaviour
{
    private Button button;
    public Color startColor = Color.white; // สีเริ่มต้นของปุ่ม
    public Color targetColor = Color.red; // สีที่ปุ่มจะเปลี่ยนไป
    public float blinkDuration = 1.0f; // ระยะเวลาในการกระพริบ (ในวินาที)

    private bool isBlinking = false;
    private float startTime;
    private bool canPressAnyKey = true;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {   if (Input.GetMouseButtonDown(0))
        {
            canPressAnyKey = true;
        
            if (isBlinking)
            {
                // คำนวณระยะเวลาที่ผ่านไป
                float elapsedTime = Time.time - startTime;

                // คำนวณเปอร์เซ็นต์ความกว้างของการกระพริบ
                float t = Mathf.PingPong(elapsedTime / blinkDuration, 1);

                // ปรับสีของปุ่มตามเปอร์เซ็นต์ความกว้าง
                button.image.color = Color.Lerp(startColor, targetColor, t);

                // เมื่อครบระยะเวลาการกระพริบ
                if (elapsedTime >= blinkDuration)
                {
                    isBlinking = false;
                    button.image.color = startColor;
                }
            }
        }
    }

    public void StartBlinking()
    {
        // เริ่มการกระพริบเมื่อคลิกปุ่ม
        isBlinking = true;
        startTime = Time.time;
    }
}