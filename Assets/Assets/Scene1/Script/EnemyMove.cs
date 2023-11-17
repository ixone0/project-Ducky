using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Vector3 _destinationPoint;
    public float _destinationRadius;
    public Transform PlayerPosition;
    public GameObject JumpScare;

    void Start()
    {
        agent.angularSpeed = 300;
        agent.acceleration = 100;
    }
    void Update()
    {
        SearchWaypoint();
        StartCoroutine(SoundWalk());
    }

    private void SearchWaypoint()
    {
        _destinationPoint = PlayerPosition.position;
        UnityEngine.AI.NavMeshHit hit;
        
        if (UnityEngine.AI.NavMesh.SamplePosition(_destinationPoint, out hit, 1f, UnityEngine.AI.NavMesh.AllAreas))
        {
            _destinationPoint = hit.position;
            agent.SetDestination(_destinationPoint);
        }
        else
        {
            if (retryAttempts < maxRetryAttempts)
            {
                retryAttempts++;
                SearchWaypoint();
            }
        }
    }

    private int retryAttempts = 0;
    private int maxRetryAttempts = 3; // Set a reasonable maximum number of retry attempts.

    
    public void UpdateSpeed(float x)
    {
        agent.speed = x;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Scene1System.GameOver = true;
        }
    }

    IEnumerator SoundWalk()
    {
        var PreviousPosition = transform.position;
        yield return new WaitForSeconds(0.0001f);
        if(transform.position != PreviousPosition)
        {
            gameObject.GetComponent<AudioSource>().enabled = true;
            Debug.Log("move");
        }
        else
        {
            gameObject.GetComponent<AudioSource>().enabled = false;
        } 
    }


}
