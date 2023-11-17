using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitems : MonoBehaviour
{

    public Rigidbody rb;
    public Collider coll;
    public Transform player, DContainer, Dpicture;

    public float pickupRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotfull;

    

    void Start()
    {
        if(!equipped)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }
        if(equipped)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotfull = true;
        }
    }


    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if(!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetKeyDown(KeyCode.E) && !slotfull) PickUp();

        if(equipped && Input.GetKeyDown(KeyCode.Q)) Drop();

        Debug.Log(pickupRange);
    }

    private void PickUp()
    {
        equipped = true;
        slotfull = true;

        transform.SetParent(DContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;
    }

    private void Drop()
    {
        equipped = false;
        slotfull = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(Dpicture.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(Dpicture.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random));
    }
}
