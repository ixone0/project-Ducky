using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestSomething : MonoBehaviour
{

    
    public float checkRadius = 3f; // Adjust the radius as needed.
    public LayerMask playerLayer; // Define the layer(s) that the player belongs to.

    private Collider[] previousColliders; // Store the previously detected players.

    public bool playerWasDetected = false;
    public bool playerStillDetected = false;

    public Color sphereColor = Color.red; // Adjust the color as needed.

    void Start()
    {
        // Initialize previousColliders with an empty array.
        previousColliders = new Collider[0];
    }

    void Update()
    {
        // Get the position of this GameObject.
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
            if (!playerWasDetected)
            {
                Debug.Log("Player entered the sphere!");
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
            if (!playerStillDetected)
            {
                Debug.Log("Player exited the sphere!");
                // Add your custom logic here for when the player exits.
            }
        }

        // Update previousColliders with the currentColliders for the next frame.
        previousColliders = currentColliders;

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        //Gizmos.DrawSphere(transform.position, checkRadius);
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
            if (!playerWasDetected)
            {
                Debug.Log("Player entered the sphere!");
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
            if (!playerStillDetected)
            {
                Debug.Log("Player exited the sphere!");
                // Add your custom logic here for when the player exits.
            }
        }

        // Update previousColliders with the currentColliders for the next frame.
        previousColliders = currentColliders;
    }

}
/*
    public float checkRadius = 5f; // Adjust the radius as needed.
    public LayerMask playerLayer; // Define the layer(s) that the player belongs to.
    public Color sphereColor = Color.red; // Adjust the color as needed.


    void Update()
    {
        // Get the position of this GameObject.
        Vector3 sphereCenter = transform.position;

        // Use OverlapSphere to check for objects within the specified radius.
        Collider[] colliders = Physics.OverlapSphere(sphereCenter, checkRadius, playerLayer);

        // Loop through the colliders to see if any of them are the player.
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // The player is within the specified radius.
                Debug.Log("Player detected!");
                // You can add your custom logic here.
            }
        }

    
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = sphereColor;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        //Gizmos.DrawSphere(transform.position, checkRadius);
    }

*/
/*
    public float radius = 2f;
    public Collider[] Player;
    
    void Update()
    {
        ShowUI();
    }

    void ShowUI()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);
        foreach (hitColliders.GameObject.name == "Player2")
        {
            Debug.Log(hitColliders.GameObject.name);
        }

        Collider[] nothitColliders = Player.Except(hitColliders).ToArray();
        foreach (var player in nothitColliders)
        {
           // Debug.Log("NotShowUI");
        }


    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }

*/

    

