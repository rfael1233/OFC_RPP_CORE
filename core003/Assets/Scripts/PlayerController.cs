using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Responsavel pela velocidade do player
    public Rigidbody2D playerRb;
    public float jumpForce; //Sera responsavel pelo força do pulo
    public bool pulo;
    public bool isgrounded;
    
    
    
    
    private float movePlayer;// Sera responsavel pelo INPUT do player
    private bool porta;
    private GameObject novaPorta;
    
    
    void Start()
    {
        novaPorta = GameObject.Find("novaPorta");
    }

    void Update()
    {
        novaPosicao();
        movePlayer = Input.GetAxis("Horizontal"); //Usando o input para atribuir o teclado com os comandos da horizontal.
        playerRb.velocity = new Vector2(movePlayer * speed, playerRb.velocity.y); //Movimentando o player, para um lado e para o outro... tanto para a esquerda, quanto para a direita.
        
        pulo = Input.GetButtonDown("Jump");//Quando os botoes de Jump.

        //Condiçao do pulo
        if (pulo == true && isgrounded == true)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
            isgrounded = false;
        }

        //Se o movimento do player estiver andando para direita, ele ira continuar andando para a direita
        if (movePlayer > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        //Se o movimento do player estiver andando para esquerda, fara 180 graus para a esquerda
        if (movePlayer < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        
        
        

    }

    //Detectando se o player está na TAG "chao"
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("chao"))
        {
            isgrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Quando o player colidir com a tag next, ira executar a função novaPosicao.
        if (col.gameObject.CompareTag("next"))
        {
            porta = true;
        }
    }

    private void novaPosicao()
    {
        if (porta == true)
        {
            playerRb.transform.position = new Vector2(novaPorta.transform.position.x, novaPorta.transform.position.y);
            porta = false;
        }
    }
    
}
