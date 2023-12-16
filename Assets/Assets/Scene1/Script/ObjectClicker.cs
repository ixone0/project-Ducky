using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClicker : MonoBehaviour
{   
    [Header("Raycast")]
    public Color RayColor = Color.blue; // Adjust the color as needed.
    public RaycastHit hit;
    public Ray ray;
    public float DistanceRay;

    [Header("UI")]
    public Text ScoreText;
    public int score = 0;
    public int maxScore = 5;

    [Header("System")]
    [SerializeField] GameObject System;
    Scene1System scene1system;

    [Header("Button1")]
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button1First;
    [SerializeField] private GameObject button1Second;

    [Header("Button2")]
    [SerializeField] private GameObject button2;
    [SerializeField] private GameObject button2First;
    [SerializeField] private GameObject button2Second;

    [Header("Button3")]
    [SerializeField] private GameObject button3;
    [SerializeField] private GameObject button3First;
    [SerializeField] private GameObject button3Second;

    [Header("Button4")]
    [SerializeField] private GameObject button4;
    [SerializeField] private GameObject button4First;
    [SerializeField] private GameObject button4Second;

    [Header("Button5")]
    [SerializeField] private GameObject button5;
    [SerializeField] private GameObject button5First;
    [SerializeField] private GameObject button5Second;


    void Start()
    {
        scene1system = System.GetComponent<Scene1System>();
        score = 0;
        DistanceRay = 5f;

        button1First.SetActive(true);
        button2First.SetActive(true);
        button3First.SetActive(true);
        button4First.SetActive(true);
        button5First.SetActive(true);
        button1Second.SetActive(false);
        button2Second.SetActive(false);
        button3Second.SetActive(false);
        button4Second.SetActive(false);
        button5Second.SetActive(false);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    void Update()
    {
        SystemClicker();    
    }

    public void SystemClicker()
    {
        ScoreText.text = "Score: " + score + " / 5";
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * DistanceRay, RayColor);
            if (Physics.Raycast(ray, out hit, DistanceRay))
            {
                if (hit.transform.name == "Button1")
                {
                    Debug.Log("HIT Button");
                    button1.name = "ButtonPhase2";
                    scene1system.Button1 = true;
                    button1First.SetActive(false);
                    button1Second.SetActive(true);
                    if(score < maxScore)
                    {
                        score += 1;
                    }
                }
                if (hit.transform.name == "Button2")
                {
                    Debug.Log("HIT Button");
                    button2.name = "ButtonPhase2";
                    scene1system.Button2 = true;
                    button2First.SetActive(false);
                    button2Second.SetActive(true);
                    if(score < maxScore)
                    {
                        score += 1;
                    }
                }
                if (hit.transform.name == "Button3")
                {
                    Debug.Log("HIT Button");
                    button3.name = "ButtonPhase2";
                    scene1system.Button3 = true;
                    button3First.SetActive(false);
                    button3Second.SetActive(true);
                    if(score < maxScore)
                    {
                        score += 1;
                    }
                }
                if (hit.transform.name == "Button4")
                {
                    Debug.Log("HIT Button");
                    button4.name = "ButtonPhase2";
                    scene1system.Button4 = true;
                    button4First.SetActive(false);
                    button4Second.SetActive(true);
                    if(score < maxScore)
                    {
                        score += 1;
                    }
                }
                if (hit.transform.name == "Button5")
                {
                    Debug.Log("HIT Button");
                    button5.name = "ButtonPhase2";
                    scene1system.Button5 = true;
                    button5First.SetActive(false);
                    button5Second.SetActive(true);
                    if(score < maxScore)
                    {
                        score += 1;
                    }
                }
            }
        }
    }

}
