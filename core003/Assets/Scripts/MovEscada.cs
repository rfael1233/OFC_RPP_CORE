using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEscada : MonoBehaviour
{
    private float vertical; //Sera o input
    public float speed = 7f; //A velocidade de subir a escada;
    private bool escada; 
    private bool escalando;

    public Rigidbody2D playerRb;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        vertical = Input.GetAxis("Vertical");// Input para subir a escada

        if (escada && Mathf.Abs(vertical) > 0f)
        {
            escalando = true;
        }
    }

    private void FixedUpdate()
    {
        //Desativando a gravidade para o player subir
        if (escalando == true)
        {
            playerRb.gravityScale = 0f;
            playerRb.velocity = new Vector2(playerRb.velocity.x, vertical * speed);
        }
        //Ativando a gravidade novamente
        else
        {
            playerRb.gravityScale = 2f;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("escada"))//ESSA ESCADA É A DA TEG
        {
            escada = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("escada"))
        {
            escada = false;
            escalando = false;
        }
    }
}
