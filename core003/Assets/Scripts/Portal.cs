using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Portal : MonoBehaviour
{
    public GameObject portalSaida;
    private GameObject playerGO;
    public bool habilitaTeleporte;
    private AudioSource _source;


    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        _source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (habilitaTeleporte && Input.GetKeyDown(KeyCode.DownArrow))
        {
            _source.Play();
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
