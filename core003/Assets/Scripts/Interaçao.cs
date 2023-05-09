using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interaçao : MonoBehaviour
{
    
    //public float distanciaMaxima = 5f;
    public Image imagemRefencia;

    //private GameObject player;
    
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        imagemRefencia.enabled = false;
        
    }
    void Update()
    {
        /*
        if (Vector3.Distance(transform.position, player.transform.position) <= distanciaMaxima)
        {
            imagemRefencia.enabled = true;
        }
        else
        {
            imagemRefencia.enabled = false;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            imagemRefencia.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagemRefencia.enabled = false;
        }
    }
}
