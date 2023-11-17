using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem3 : MonoBehaviour
{
    public GameObject Dpicture;
    public Transform DpictureParent;

    void Start()
    {
        Dpicture.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G))
        {
            Drop();
        }
    }

    void Drop()
    {
        DpictureParent.DetachChildren();
        Dpicture.transform.eulerAngles = new Vector3(Dpicture.transform.position.x, Dpicture.transform.position.z, Dpicture.transform.position.y);
        Dpicture.GetComponent<Rigidbody>().isKinematic = false;
        Dpicture.GetComponent<MeshCollider>().enabled = true;
        Dpicture.GetComponent<BoxCollider>().enabled = true;
    }

    void Equip()
    {
        Dpicture.GetComponent<Rigidbody>().isKinematic = true;

        Dpicture.transform.position = DpictureParent.transform.position;
        Dpicture.transform.rotation = DpictureParent.transform.rotation;

        Dpicture.GetComponent<MeshCollider>().enabled = false;
        Dpicture.GetComponent<BoxCollider>().enabled = false;

        Dpicture.transform.SetParent(DpictureParent);
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("press E pick item");
            if(Input.GetKey(KeyCode.E))
            {
                Equip();
            }
        }
    }
}
