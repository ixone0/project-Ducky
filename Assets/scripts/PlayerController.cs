using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float y, x;
    private Rigidbody rb;

    public bool grounded;

    public float walkSpeed = 5f, sensitivity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(rb.transform.position, Vector3.down, Camera.main.transform.localPosition.y + .5f);
        if(Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
        Look();
    }


    void Look()
    {
        x -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        x = Mathf.Clamp(x, -90, 90);
        y += Input.GetAxisRaw("Mouse X") * sensitivity;
        Camera.main.transform.localRotation = Quaternion.Euler(x, y, 0);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized * walkSpeed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0, Camera.main.transform.right.x);
        Vector3 moveDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = moveDirection;
    }
}
