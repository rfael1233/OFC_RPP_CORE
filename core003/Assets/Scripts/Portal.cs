using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Portal : MonoBehaviour
{
    public GameObject portalSaida;
    private GameObject playerGO;
    private bool habilitaTeleporte;
    
    
    
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (habilitaTeleporte == true && Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerGO.transform.position = portalSaida.transform.position;
            habilitaTeleporte = false;
        }
        if(habilitaTeleporte && Input.GetKeyDown(KeyCode.S))
        {
            playerGO.transform.position = portalSaida.transform.position;
            habilitaTeleporte = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            habilitaTeleporte = true;
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            habilitaTeleporte = false;
        }
        
    }
}
