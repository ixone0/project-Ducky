using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    public LayerMask enemyLayer;
    public string enemyTag = "Enemy";
    public LayerMask obstacleLayer;
    public float maxRayDistance = 100f;
    public Transform CameraPlayer;
    public GameObject EnemyOb;
    private EnemyMove enemymove;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
        enemymove = EnemyOb.GetComponent<EnemyMove>();
    }

    private void Update()
    {
        CheckForEnemiesInSight();
    }

    private void CheckForEnemiesInSight()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, maxRayDistance, enemyLayer);

        foreach (Collider enemy in enemies)
        {
            // Get the enemy's position in screen space
            Vector3 screenPos = playerCamera.WorldToViewportPoint(enemy.transform.position);

            // Check if the enemy is within the camera's view (screen aspect)
            if (screenPos.x >= 0f && screenPos.x <= 1f && screenPos.y >= 0f && screenPos.y <= 1f && screenPos.z > 0f)
            {
                // Check if there's an obstacle between the player and the enemy
                if (!IsObstacleInPath(enemy.transform))
                {
                    Debug.Log("See enemy: " + enemy.name);
                    enemymove.UpdateSpeed(0f);
                }
                else
                {
                    Debug.Log("Don't see enemy: " + enemy.name);
                    enemymove.UpdateSpeed(10f);
                }
            }
            else
            {
                Debug.Log("Don't see enemy: " + enemy.name);
                enemymove.UpdateSpeed(10f);
            }
        }
    }

    private bool IsObstacleInPath(Transform target)
    {
        Vector3 directionToTarget = target.position - CameraPlayer.position;

        Debug.DrawRay(CameraPlayer.position, directionToTarget, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(CameraPlayer.position, directionToTarget, out hit, maxRayDistance, obstacleLayer))
        {
            // Draw a red debug ray to show the raycast hit point
            Debug.DrawRay(CameraPlayer.position, directionToTarget.normalized * hit.distance, Color.red);
            return true; // An obstacle is in the way
        }

        // Draw a debug ray to the maxRayDistance if no obstacle was hit
        Debug.DrawRay(CameraPlayer.position, directionToTarget.normalized * maxRayDistance, Color.green);

        return false; // No obstacles in the way
    }
}