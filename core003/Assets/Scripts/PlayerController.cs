using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int vida;
    private int vidaMaxima = 3;

    [SerializeField] private Image vidaOn;
    [SerializeField] private Image vidaOff;
    
    [SerializeField] private Image vidaOn2;
    [SerializeField] private Image vidaOff2;
    
    



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
        vida = vidaMaxima;
        novaPorta = GameObject.Find("novaPorta");//novaPorta é nome que está no projeto
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

        if (col.gameObject.CompareTag("enemy"))
        {
            Dano();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("espinho"))
        {
            Dano();
        }
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

    private void Dano()
    {
        vida -= 1;           //tirando dano do player

        if (vida == 2)
        {
            vidaOn2.enabled = true;
            vidaOff2.enabled = false;
        }
        else
        {
            vidaOn2.enabled = false;
            vidaOff2.enabled = true;
        }

        if (vida == 1)
        {
            //So para garantir que o primeiro coraçao esta apagado mesmo
            vidaOn2.enabled = true;
            vidaOff2.enabled = false;

            vidaOn.enabled = true;
            vidaOff.enabled = false;
        }
        else
        {
            vidaOn.enabled = false;
            vidaOff.enabled = true;
        }

        if (vida <= 0)
        {
            SceneManager.LoadScene(6);
            
        }
        
        
        
        
        
    }
    
}
