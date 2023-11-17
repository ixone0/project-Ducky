using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayPlayer : MonoBehaviour
{
    
    public float checkRadiusRoom = 1f;
    public LayerMask playerLayer; // Define the layer(s) that the player belongs to.
    public LayerMask RoomDuck;
    public LayerMask RoomCat;
    public LayerMask RoomClown;
    public LayerMask RoomPig;
    public Color sphereColor = Color.blue; // Adjust the color as needed.
    public bool InRoomDuck = false;
    public bool InRoomCat = false;
    public bool InRoomClown = false;
    public bool InRoomPig = false;
    private bool attachedInRoomDuck = false;
    private bool attachedInRoomCat = false;
    private bool attachedInRoomClown = false;
    private bool attachedInRoomPig = false;

    void Start()
    {
        InRoomDuck = false;
        InRoomCat = false;
        InRoomClown = false;
        InRoomPig = false;
        attachedInRoomDuck = false;
        attachedInRoomCat = false;
        attachedInRoomClown = false;
        attachedInRoomPig = false;
    }

    void Update()
    {
        AttachRoom();
        CheckInRoom();
    }

    private void CheckInRoom()
    {
        Vector3 center = transform.position; 

        Collider[] colliderduck = Physics.OverlapSphere(center, checkRadiusRoom, RoomDuck);

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

        Collider[] collidercat = Physics.OverlapSphere(center, checkRadiusRoom, RoomCat);

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

        Collider[] colliderclown = Physics.OverlapSphere(center, checkRadiusRoom, RoomClown);

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

        Collider[] colliderpig = Physics.OverlapSphere(center, checkRadiusRoom, RoomPig);

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
        Gizmos.DrawWireSphere(transform.position, checkRadiusRoom);
        //Gizmos.DrawSphere(transform.position, checkRadius);
    }

    private void AttachRoom()
    {
        if(InRoomDuck && !DoorOpenClose.DoorDuckisOpen)
        {
            gameObject.transform.localPosition = new Vector3(
                -35.73f,
                2.67f,
                -21.36f);
        }

        if(InRoomCat && !DoorOpenClose.DoorCatisOpen)
        {
            gameObject.transform.localPosition = new Vector3(
                -32.37f,
                2.67f,
                59.71f);
        }

        if(InRoomClown && !DoorOpenClose.DoorClownisOpen)
        {
            gameObject.transform.localPosition = new Vector3(
                -7.76f,
                2.67f,
                55.96f);
        }

        if(InRoomPig && !DoorOpenClose.DoorPigisOpen)
        {
            gameObject.transform.localPosition = new Vector3(
                -21.22f,
                2.67f,
                33.06f);
        }
    }

}
