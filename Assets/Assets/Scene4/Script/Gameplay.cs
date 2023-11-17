using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] GameObject SuperDuck;
    public bool enemycanmove = false;
    [SerializeField] private LayerMask LayerSuperDuck;

    [Header("Player")]
    [SerializeField] private float radiusPlayer = 5;
    
    void Start()
    {
        enemycanmove = false;
    }

    private void OverLapShere()
    {
        Vector3 center = transform.position;
        Collider[] colliderPlayer = Physics.OverlapSphere(center, radiusPlayer, LayerSuperDuck);

 
    }
}
