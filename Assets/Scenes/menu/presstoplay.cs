using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class presstoplay : MonoBehaviour
{
    public GameObject animatorToPlay; // Animator ของ GameObject ที่คุณต้องการเล่นแอนิเมชัน
    private Text textComponent;
    public float fadeDuration = 1.0f; // ระยะเวลาในการเบลนด์ (ในวินาที)
    private float currentAlpha = 1.0f;
    private bool isFadingOut = false;
    private bool isTextActive = true;
    private bool canPressAnyKey = true;
    public AudioSource SoundWhenStart;
    
    private void Start()
    {
        textComponent = GetComponent<Text>();
        InvokeRepeating("ToggleFade", 0f, fadeDuration);
    }

    private void Update()
    {   if (canPressAnyKey)
        {
            if (Input.anyKeyDown)
            {   
                isTextActive = !isTextActive;
                textComponent.enabled = isTextActive;
                PlayAnimation();
                
            }
            // ปรับค่า alpha ของข้อความเพื่อทำเบลนด์
            if (isFadingOut)
            {
                currentAlpha -= Time.deltaTime / fadeDuration;
            }
            else
            {
                currentAlpha += Time.deltaTime / fadeDuration;
            }

            currentAlpha = Mathf.Clamp01(currentAlpha);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, currentAlpha);
        }
    }

    private void ToggleFade()
    {
        isFadingOut = !isFadingOut;
    }
    public void PlayAnimation()
    {
        animatorToPlay.GetComponent<Animator>().enabled = true;
        StartCoroutine(DelaySceneChange());
    }
    IEnumerator DelaySceneChange()
    { 
        SoundWhenStart.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("stage1");
    }

}