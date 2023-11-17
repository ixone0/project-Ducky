using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckGamePlay : MonoBehaviour
{
    [Header("Raycast")]
    public float rangeF = 1;
    public float rangeB = 1;
    public LayerMask floor;

    [Header("Phase1")]
    public float RanNum1;
    public bool TimeStartF1;
    public float timeF1;
    public bool TimeStartB1;
    public float timeB1;
    public bool TimeFinishF1;
    public bool TimeFinishB1;
    public float RanNum2;
    public bool TimeFinishF2;
    public bool TimeFinishB2;
    public float RanNum3;
    public bool TimeFinishF3;
    public bool TimeFinishB3;
    public float RanNum4;
    public bool TimeFinishF4;
    public bool TimeFinishB4;
    
    [Header("Tools")]
    DuckPicture duckpicture;
    [SerializeField] GameObject gameObject;
    DoorOpenClose dooropenclose;
    [SerializeField] GameObject Player;

    [Header("UI")]
    public GameObject UIGAMEOVER;
    public GameObject UIEMOJI0;
    public GameObject UIEMOJI1;
    public GameObject UIEMOJI2;
    public GameObject UIEMOJI3;

    [Header("GameObject")]
    private MeshRenderer Mesh;
    public GameObject Shake;
    public GameObject Hand;
    public GameObject Head;

    [Header("GameOver")]
    public GameObject TimelineJumpDuck;



    void Start()
    {
        duckpicture = gameObject.GetComponent<DuckPicture>();
        dooropenclose = Player.GetComponent<DoorOpenClose>();
        RanNum1 = Random.Range(20, 40);
        RanNum2 = RanNum1 + Random.Range(30, 60);
        RanNum3 = RanNum2 + Random.Range(30, 50);
        RanNum4 = RanNum3 + Random.Range(15,25);
        TimeStartF1 = false;
        timeF1 = 0f;
        TimeStartB1 = false;
        timeB1 = 0f;
        TimeFinishB1 = false;
        TimeFinishF1 = false;
        TimeFinishF3 = false;
        TimeFinishB3 = false;
        Mesh = gameObject.GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(!duckpicture.equipped && !duckpicture.FinishThisPic)
        {
            RayCheck();
        }
        Phase();
        Setting();
        SettingEmoji();
    }

    void Setting()
    {
        if(TimeStartF1 && ScriptStartGame.StartGame)
        {
            timeF1 += Time.deltaTime;
        }

        if(TimeStartB1 && ScriptStartGame.StartGame)
        {
            timeB1 += Time.deltaTime;
        }
//----------------------------------------
        if(timeF1 >= RanNum1 && timeF1 < RanNum2)
        {
            TimeFinishF1 = true;
        }

        if(timeB1 >= RanNum1 && timeB1 < RanNum2)
        {
            TimeFinishB1 = true;
        }
//---------------------------------------------
        if(timeF1 >= RanNum2)
        {
            TimeFinishF2 = true;
            TimeFinishF1 = false;
        }

        if(timeB1 >= RanNum2)
        {
            TimeFinishB2 = true;
            TimeFinishB1 = false;
        }
//---------------------------------------------
        if(timeF1 >= RanNum3)
        {
            TimeFinishF3 = true;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
        }
        if(timeB1 >= RanNum3)
        {
            TimeFinishB3 = true;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
        }
//---------------------------------------------
        if(timeF1 >= RanNum4)
        {
            TimeFinishF4 = true;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishB3 = false;
        }     
        if(timeB1 >= RanNum4)
        {
            TimeFinishB4 = true;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
        }   
        
//Reset time

        if(duckpicture.InRoomDuck && dooropenclose.timeduck >= 9)
        {
            timeF1 = 0;
            timeB1 = 0;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishF3 = false;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
        }
        
        if(duckpicture.InRoomCat && dooropenclose.timecat >= 9)
        {
            timeF1 = 0;
            timeB1 = 0;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishF3 = false;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
        }

        if(duckpicture.InRoomClown && dooropenclose.timeclown >= 9)
        {
            timeF1 = 0;
            timeB1 = 0;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishF3 = false;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
        }

        if(duckpicture.InRoomPig && dooropenclose.timepig >= 9)
        {
            timeF1 = 0;
            timeB1 = 0;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishF3 = false;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
        }

        if(duckpicture.FinishThisPic)
        {
            TimeStartF1 = false;
            TimeStartB1 = false;
        }

        if(duckpicture.equipped)
        {
            TimeStartF1 = true;
            TimeStartB1 = true;
        }
    }

    void RayCheck()
    {
        Vector3 directionF = Vector3.left;
        Ray theRayF = new Ray(transform.position, transform.TransformDirection(directionF * rangeF));
        Debug.DrawRay(transform.position, transform.TransformDirection(directionF * rangeF), Color.red);

        RaycastHit hit;
        if (Physics.Raycast(theRayF, out hit, rangeF, floor))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("floor"))
            {
                TimeStartF1 = false;
            }
        }

        if (!Physics.Raycast(theRayF, out hit, rangeF, floor))
        {
            TimeStartF1 = true;
        }
//------------------------------------------------------------------------------------------------------------------------
        Vector3 directionB = Vector3.right;
        Ray theRayB = new Ray(transform.position, transform.TransformDirection(directionB * rangeB));
        Debug.DrawRay(transform.position, transform.TransformDirection(directionB * rangeB), Color.green);
        
        if (Physics.Raycast(theRayB, out hit, rangeB, floor))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("floor"))
            {
                TimeStartB1 = false;
            }
        }
        if (!Physics.Raycast(theRayB, out hit, rangeB, floor))
        {
            TimeStartB1 = true;
        }
    }

    void Phase()
    {   
        // F1 & B1 is mean "phase 1 Front & Back
        //Duck Model
        if(!TimeFinishB1 && !TimeFinishB2 && !TimeFinishB3 && !TimeFinishB4 && !TimeFinishF1 && !TimeFinishF2 && !TimeFinishF3 && !TimeFinishF4)
        {
            Mesh.enabled = true;
            Shake.SetActive(false);
            Hand.SetActive(false);
            Head.SetActive(false);
        }
        if(TimeFinishF1 || TimeFinishB1)
        {
            Mesh.enabled = false;
            Shake.SetActive(true);
        }
        if(TimeFinishF2 || TimeFinishB2)
        {
            TimeFinishF1 = false;
            TimeFinishB1 = false;
            Mesh.enabled = false;
            Shake.SetActive(false);
            Hand.SetActive(true);
            Head.SetActive(false);
            
        }
        if(TimeFinishF3 || TimeFinishB3)
        {
            Mesh.enabled = false;
            TimeFinishF2 = false;
            TimeFinishB2 = false;
            Mesh.enabled = false;
            Shake.SetActive(false);
            Hand.SetActive(false);
            Head.SetActive(true);
        }
        if(TimeFinishF4 & !Systemnaja.GameOver || TimeFinishB4 & !Systemnaja.GameOver)
        {
            TimeFinishF3 = false;
            TimeFinishB3 = false;
            Systemnaja.GameOver = true;
            StartCoroutine(GAMEOVER());
        }

    //--------------------------------------------------------

        if(duckpicture.FinishThisPic)
        {   
            timeF1 = 0f;
            timeB1 = 0f;
            TimeStartF1 = false;
            TimeStartB1 = false;
            TimeFinishF1 = false;
            TimeFinishF2 = false;
            TimeFinishF3 = false;
            TimeFinishB1 = false;
            TimeFinishB2 = false;
            TimeFinishB3 = false;
            UIEMOJI0.SetActive(false);
            UIEMOJI1.SetActive(false);
            UIEMOJI2.SetActive(false);
            UIEMOJI3.SetActive(false);
         }
    }

    IEnumerator GAMEOVER()
    {
        //JUMP Duck
        TimelineJumpDuck.SetActive(true);
        Debug.Log("Duck GameOver");
        yield return new WaitForSeconds(2.0f);
        TimelineJumpDuck.SetActive(false);
        var original = Time.timeScale;
        Time.timeScale = 0f;
        UIGAMEOVER.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void SettingEmoji()
    {
        if(!TimeFinishB1 & !TimeFinishB2 & !TimeFinishB3 & !TimeFinishB4 & !TimeFinishF1 & !TimeFinishF2 & !TimeFinishF3 & !TimeFinishF4)
        {
            UIEMOJI0.SetActive(true);
            UIEMOJI1.SetActive(false);
            UIEMOJI2.SetActive(false);
            UIEMOJI3.SetActive(false);
        }
        if(TimeFinishF1 || TimeFinishB1)
        {
            UIEMOJI0.SetActive(false);
            UIEMOJI1.SetActive(true);
            UIEMOJI2.SetActive(false);
            UIEMOJI3.SetActive(false);
        }
        if(TimeFinishF2 || TimeFinishB2)
        {
            UIEMOJI0.SetActive(false);
            UIEMOJI1.SetActive(false);
            UIEMOJI2.SetActive(true);
            UIEMOJI3.SetActive(false);
        }
        if(TimeFinishF3 || TimeFinishB3)
        {
            UIEMOJI0.SetActive(false);
            UIEMOJI1.SetActive(false);
            UIEMOJI2.SetActive(false);
            UIEMOJI3.SetActive(true);
        }
    }
}
