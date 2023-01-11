using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portalSaida;
    private GameObject playerGO;
    
    
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerGO.transform.position = portalSaida.transform.position - new Vector3(0, 0.7f, 0);
        }

    
    }
}
