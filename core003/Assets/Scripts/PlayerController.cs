using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Responsavel pela velocidade do player
    public Rigidbody2D playerRb; //Corpo do Player
    public float jumpForce; //Sera responsavel pelo força do pulo
    public bool pulo; 
    public bool isgrounded; // Check
    private bool flipX;
    
    //Sistrema de dash

    private bool canDash = true; //Posso dá o dash?
    private bool isDashing; // Dash ativo.
    public float dashingPower = 200f; //Responsavel pela força do dash
    public float dashingTime = 0.2f; // Responsavel pelo tempo do dash
    public float dashingCoolDown = 0.5f; //Responsavel pelo controle de tempo espera ate pode dá outro dash
    
    
    private float movePlayer;// Sera responsavel pelo INPUT do player
    private bool porta;
    private GameObject novaPorta;
    
    
    void Start()
    {
        novaPorta = GameObject.Find("novaPorta");
    }

    void FixedUpdate()
    {
        if (isDashing == true)
        {
            return;
        }
        playerRb.velocity = new Vector2(movePlayer * speed, playerRb.velocity.y); //Movimentando o player, para um lado e para o outro... tanto para a esquerda, quanto para a direita.
        
        
    }

    void Update()
    {
        novaPosicao();
        movePlayer = Input.GetAxis("Horizontal"); //Usando o input para atribuir o teclado com os comandos da horizontal.
        pulo = Input.GetButtonDown("Jump");//Quando os botoes de Jump.
        
        
        //Só para garantir que tenha retorno do isDashing
        if (isDashing == true)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F) && canDash == true)
        {
            StartCoroutine(Dash());
        }
        
        
        
        
        //Condiçao do pulo
        if (pulo == true && isgrounded == true)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
            isgrounded = false;
        }

        
        if (flipX == false && movePlayer < 0)
        {
            Flip();
        }
        else if(flipX == true && movePlayer > 0)
        {
            Flip();
        }
        
        
         
       
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;


    }

    private void Flip()
    {
        flipX = !flipX;
        float x = transform.localScale.x;

        x *= -1;
        transform.localScale = new Vector3( x, transform.localScale.y, transform.localScale.z);
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
