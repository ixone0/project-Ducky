using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : MonoBehaviour
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
    Systemnaj systemnaja;
    [SerializeField] GameObject GameSystem;
    CatPicture catpicture;
    [SerializeField] GameObject CatObject;

    [Header("OverlapSphere")]
    public float checkRadius = 3f; // Adjust the radius as needed.
    public LayerMask playerLayer; // Define the layer(s) that the player belongs to.
    public LayerMask WallLayer;
    private Collider[] previousColliders; // Store the previously detected players.
    public bool playerWasDetected = false;
    public bool playerStillDetected = false;
    public Color sphereColor = Color.red; // Adjust the color as needed.
    public bool InCollPic = false;
    public bool FinishThisPic = false;
    public bool InCollWall = false;

    public void Awake()
    {
        systemnaja = GameSystem.GetComponent<Systemnaj>();
        catpicture = CatObject.GetComponent<CatPicture>();
        equipped = false;
        Slotfull = false;
        slotfull = false;
        InCollPic = false;
        FinishThisPic = false;
        previousColliders = new Collider[0];
        InCollWall = false;
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

    public void PickUp()
    {
        equipped = true;
        Slotfull = true;
        CatPicture.Slotfull = true;
        ClownPicture.Slotfull = true;
        PigPicture.Slotfull = true;
        trash.Slotfull = true;

        transform.SetParent(DContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        PickupUI.SetActive(false);
    }

    public void Drop()
    {
        equipped = false;
        Slotfull = false;
        CatPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;
        trash.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.AddForce(Dpicture.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Dpicture.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random));
    }

    public void PutOnWall1()
    {
        equipped = false;
        Slotfull = false;
        CatPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;
        trash.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall1);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.BinComplete1 = true;
        PickupUI.SetActive(false);
        PlaceUI.SetActive(false);
        ONWALL1.GetComponent<BoxCollider>().enabled = false;
        FinishThisPic = true;

    }

    public void PutOnWall2()
    {
        equipped = false;
        Slotfull = false;
        CatPicture.Slotfull = false;
        ClownPicture.Slotfull = false;
        PigPicture.Slotfull = false;
        trash.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall2);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.BinComplete2 = true;
        PickupUI.SetActive(false);
        PlaceUI.SetActive(false);
        ONWALL2.GetComponent<BoxCollider>().enabled = false;
        FinishThisPic = true;
    }

    public void PutOnWall3()
    {
        equipped = false;
        Slotfull = false;
        trash.Slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = true;
        coll.isTrigger = false;

        transform.SetParent(OnWall3);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        systemnaja.BinComplete3 = true;
        PickupUI.SetActive(false);
        PlaceUI.SetActive(false);
        ONWALL3.GetComponent<BoxCollider>().enabled = false;
        FinishThisPic = true;
    }

    public void Setting()
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

        // Use OverlapSphere to check for objects within the specified radius.
        Collider[] currentColliders = Physics.OverlapSphere(sphereCenter, checkRadius, playerLayer);

        // Check for players that have entered the overlap sphere.
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

            // If the current player collider was not detected in the previous frame,
            // the player has entered the sphere.
            if (!playerWasDetected && !FinishThisPic)
            {
                Debug.Log("Player entered the sphere!");
                InCollPic = true;
                PickupUI.SetActive(true);
                // Add your custom logic here for when the player enters.
            }
        }

        // Check for players that have exited the overlap sphere.
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

            // If the previous player collider is no longer in the current colliders list,
            // the player has exited the sphere.
            if (!playerStillDetected && !FinishThisPic)
            {
                Debug.Log("Player exited the sphere!");
                InCollPic = false;
                PickupUI.SetActive(false);
                // Add your custom logic here for when the player exits.
            }
        }
        // Update previousColliders with the currentColliders for the next frame.
        previousColliders = currentColliders;

        //-----------------------------------------------------------------------------------------
        Collider[] currentWalls = Physics.OverlapSphere(sphereCenter, checkRadius, WallLayer);
        foreach (Collider currentWall in currentWalls)
        {
            bool PictureDectectWall = false;

            foreach (Collider previousCollider in previousColliders)
            {
                if (previousCollider == currentWall)
                {
                    PictureDectectWall = true;
                    break;
                }
            }

            if (!PictureDectectWall)
            {
                Debug.Log("Picture Hit Wall");
                InCollWall = true;
            }
        }
        foreach (Collider previousCollider in previousColliders)
        {
            bool PictureStillOnWall = false;

            foreach (Collider currentWall in currentWalls)
            {
                if (previousCollider == currentWall)
                {
                    PictureStillOnWall = true;
                    break;
                }
            }

            if (!PictureStillOnWall && !FinishThisPic)
            {
                Debug.Log("Picture not still in On wall");
                PlaceUI.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        //Gizmos.DrawSphere(transform.position, checkRadius);
    }
}

