using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPicture : MonoBehaviour
{
    
    [Header("Component")]
    public Rigidbody rb;
    public Collider coll;
    public GameObject PickupUI;
    public GameObject PlaceUI;
    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    [Header("Container")]
    public Transform player;
    public Transform DContainer;
    public Transform Dpicture;
    public Transform OnWall1;
    public Transform OnWall2;
    public Transform OnWall3;

    [Header("Boolean")]
    public bool equipped;
    public static bool Slotfull;
    public bool slotfull;
    public GameObject ONWALL1;
    public GameObject ONWALL2;
    public GameObject ONWALL3;
    DuckPicture duckpicture;
    [SerializeField] GameObject DuckObject;
    Systemnaja systemnaja;
    [SerializeField] GameObject GameSystem;

    [Header("OverlapSphere")]
    public float checkRadius = 3f; // Adjust the radius as needed.
    public float checkRadiusRoom = 1f;
    public LayerMask playerLayer; // Define the layer(s) that the player belongs to.
    public LayerMask WallLayer;
    public LayerMask RoomDuck;
    public LayerMask RoomCat;
    public LayerMask RoomClown;
    public LayerMask RoomPig;
    private Collider[] previousColliders; // Store the previously detected players.
    public bool playerWasDetected = false;
    public bool playerStillDetected = false;
    public Color sphereColor = Color.blue; // Adjust the color as needed.
    public bool InCollPic = false;
    public bool FinishThisPic = false;
    public bool InCollWall = false;
    public bool InRoomDuck = false;
    public bool InRoomCat = false;
    public bool InRoomClown = false;
    public bool InRoomPig = fal se;
    private bool attachedToWall = false; // Flag to track attachment status
    private bool attachedInRoomDuck = false;
    private bool attachedInRoomCat = false;
    private bool attachedInRoomClown = false;
    private bool attachedInRoomPig = false;

    [Header("Audio")]
    public AudioSource SoundPickup;
    public AudioSource SoundDrop;
    public AudioSource SoundPutonwall;

    [Header("Emoji")]
    public GameObject Emoji;

    public void Start()
    {
        systemnaja = GameSystem.GetComponent<Systemnaja>();
        duckpicture = DuckObject.GetComponent<DuckPicture>();
        equipped = false;
        Slotfull = false;
        slotfull = false;
        InCollPic = false;
        FinishThisPic = false;
        previousColliders = new Collider[0];
        InCollWall = false;
        InRoomDuck = false;
        InRoomCat = false;
        InRoomClown = false;
        InRoomPig = false;
        attachedInRoomDuck = false;
        attachedInRoomCat = false;
        attachedInRoomClown = false;
        attachedInRoomPig = false;
        if(!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if(equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            Slotfull = true;
        }
        rb.isKinematic = true;
    }

    public void Update()
    {
        if(!FinishThisPic)
        {
            //Vector3 distanceToPlayer = player.position - transform.position;
            Vector3 distanceToContainer1 = transform.position - OnWall1.position;
            Vector3 distanceToContainer2 = transform.position - OnWall2.position;
            Vector3 distanceToContainer3 = transform.position - OnWall3.position;
            Setting();
            OverLap();
            CheckForAttachment();
            CheckInRoom();

            if(!equipped && Input.GetKeyDown(KeyCode.E) && !Slotfull && InCollPic) PickUp();

            if(equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
            
            
            if(equipped && InCollWall && distanceToContainer1.magnitude <= pickupRange)
            {
                PlaceUI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    PutOnWall1();

                }
            }
            if(equipped && InCollWall && distanceToContainer2.magnitude <= pickupRange)
            {
                PlaceUI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    PutOnWall2();

                }
            }
            if(equipped && InCollWall && distanceToContainer3.magnitude <= pickupRange)
            {
                PlaceUI.SetActive(true);
                if(Input.GetKeyDown(KeyCode.F))
                {
                    PutOnWall3();
                }
            }
        }

    }

    void PickUp()
    {
        equipped = true;
        Slotfull = true;
        DuckPicture.Slotfull = true;
        ClownPicture.Slotfull = true;
        PigPicture.Slotfull = true;

        transform.SetParent(DContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        SoundPickup.Play();

        PickupUI.SetActive(false);
    }


    void Drop()
    {
        equipped = false;
        Slotfull = false;
        DuckPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.AddForce(Dpicture.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Dpicture.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random));

        SoundDrop.Play();
    }

    public void PutOnWall1()
    {
        equipped = false;
        Slotfull = false;
        DuckPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall1);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.CatComplete1 = true;
        PickupUI.SetActive(false);      
        PlaceUI.SetActive(false);
        ONWALL1.GetComponent<BoxCollider>().enabled = false;
        SoundPutonwall.Play();
        Emoji.SetActive(false);
        FinishThisPic = true;

    }

    public void PutOnWall2()
    {
        equipped = false;
        Slotfull = false;
        DuckPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall2);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.CatComplete2 = true;
        PickupUI.SetActive(false);
        PlaceUI.SetActive(false);
        ONWALL2.GetComponent<BoxCollider>().enabled = false;
        SoundPutonwall.Play();
        Emoji.SetActive(false);
        FinishThisPic = true;
    }

    public void PutOnWall3()
    {
        equipped = false;
        Slotfull = false;
        DuckPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall3);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.CatComplete3 = true;
        PickupUI.SetActive(false);
        PlaceUI.SetActive(false);
        ONWALL3.GetComponent<BoxCollider>().enabled = false;
        SoundPutonwall.Play();
        Emoji.SetActive(false);
        FinishThisPic = true;
    }

    void Setting()
    {
        if(Slotfull)
        {
            slotfull = true;
        }
        if(!Slotfull)
        {
            slotfull = false;
        }
    }

    void OverLap()
    {
        Vector3 sphereCenter = transform.position;

        
        Collider[] currentColliders = Physics.OverlapSphere(sphereCenter, checkRadius, playerLayer);
        foreach (Collider currentCollider in currentColliders)
        {
            playerWasDetected = false;

            foreach (Collider previousCollider in previousColliders)
            {
                if (previousCollider == currentCollider)
                {
                    playerWasDetected = true;
                    break;
                }
                
            }

           
            if (!playerWasDetected && !FinishThisPic)
            {
                Debug.Log("Player entered the sphere!");
                InCollPic = true;
                PickupUI.SetActive(true);

            }
        }

     
        foreach (Collider previousCollider in previousColliders)
        {
            playerStillDetected = false;

            foreach (Collider currentCollider in currentColliders)
            {
                if (previousCollider == currentCollider)
                {
                    playerStillDetected = true;
                    break;
                }
            }

           
            if (!playerStillDetected && !FinishThisPic)
            {
                Debug.Log("Player exited the sphere!");
                InCollPic = false;
                PickupUI.SetActive(false);
               
            }
        }
        previousColliders = currentColliders;
    }
    
    private void CheckForAttachment()
    {
        Vector3 center = transform.position; 

        Collider[] colliders = Physics.OverlapSphere(center, checkRadius, WallLayer );

        if (colliders.Length > 0)
        {
            if (!attachedToWall)
            {
                attachedToWall = true; 
                InCollWall = true;
            }
        }
        else
        {
           
            if (attachedToWall)
            {
                attachedToWall = false; 
                InCollWall = false;
                PlaceUI.SetActive(false);
            }
        }
        
    }

    private void CheckInRoom()
    {
        Vector3 center = transform.position; 

        Collider[] colliderduck = Physics.OverlapSphere(center, checkRadius, RoomDuck);

        if (colliderduck.Length > 0)
        {
            if (!attachedInRoomDuck)
            {
                attachedInRoomDuck = true; 
                InRoomDuck = true;
            }
        }
        else
        {
           
            if (attachedInRoomDuck)
            {
                attachedInRoomDuck = false; 
                InRoomDuck = false;
            }
        }

        Collider[] collidercat = Physics.OverlapSphere(center, checkRadius, RoomCat);

        if (collidercat.Length > 0)
        {
            if (!attachedInRoomCat)
            {
                attachedInRoomCat = true; 
                InRoomCat = true;
            }
        }
        else
        {
           
            if (attachedInRoomCat)
            {
                attachedInRoomCat = false; 
                InRoomCat = false;
            }
        }

        Collider[] colliderclown = Physics.OverlapSphere(center, checkRadius, RoomClown);

        if (colliderclown.Length > 0)
        {
            if (!attachedInRoomClown)
            {
                attachedInRoomClown = true; 
                InRoomClown = true;
            }
        }
        else
        {
           
            if (attachedInRoomClown)
            {
                attachedInRoomClown = false; 
                InRoomClown = false;
            }
        }

        Collider[] colliderpig = Physics.OverlapSphere(center, checkRadius, RoomPig);

        if (colliderpig.Length > 0)
        {
            if (!attachedInRoomPig)
            {
                attachedInRoomPig = true; 
                InRoomPig = true;
            }
        }
        else
        {
           
            if (attachedInRoomPig)
            {
                attachedInRoomPig = false; 
                InRoomPig = false;
            }
        }


        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawWireSphere(transform.position, checkRadiusRoom);
        //Gizmos.DrawSphere(transform.position, checkRadius);
    }
}