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

    [SerializeField] private Image vidaOn3;
    [SerializeField] private Image vidaOff3;


    [SerializeField] private GameObject shieldObject;
    

    public float speed; // Responsavel pela velocidade do player
    public Rigidbody2D playerRb; //Corpo do Player
    public float jumpForce; //Sera responsavel pelo força do pulo
    public bool pulo; 
    public bool isgrounded; // Check
    private bool flipX;
    
    public Animator animator;
    
    //Sistrema de dash

    private bool canDash = true; //Posso dá o dash?
    private bool isDashing; // Dash ativo.
    public float dashingPower = 200f; //Responsavel pela força do dash
    public float dashingTime = 0.2f; // Responsavel pelo tempo do dash
    public float dashingCoolDown = 0.5f; //Responsavel pelo controle de tempo espera ate pode dá outro dash

    private bool canShield = true;
    private bool isShielding;
    public float shieldTime = 2f;
    public float shieldCooldown = 0.5f;
    
    private float movePlayer;// Sera responsavel pelo INPUT do player
    private bool porta;
    private GameObject novaPorta;

    private bool isDead;

    private GameController gcPlayer;
    
    void Start()
    {
        vida = vidaMaxima;
        novaPorta = GameObject.Find("novaPorta");//novaPorta é nome que está no projeto
        animator = GetComponent<Animator>();
        gcPlayer = GameController.gc;
        gcPlayer.core = 0;

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
        if (Input.GetAxis("Horizontal") != 0)
        {
            //esta correndo
            animator.SetBool("taCorrendo", true);
        }
        else
        {
            //esta parado
            animator.SetBool("taCorrendo", false);
        }
       
        movePlayer = Input.GetAxis("Horizontal"); //Usando o input para atribuir o teclado com os comandos da horizontal.
        pulo = Input.GetButtonDown("Jump");//Quando os botoes de Jump.
        
        
        //Só para garantir que tenha retorno do isDashing
        
        if (Input.GetKeyDown(KeyCode.F) && canDash == true)
        {
            if (!isDashing)
            {
                StartCoroutine(Dash());
                
            }
            //animator.SetBool("dashAtivo", true);
            
           
            
        }

        
        //Condiçao do pulo
        if (pulo == true && isgrounded == true)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
            isgrounded = false;
            animator.SetBool("taPulando",true);
            //ativando a animação
        }

        if (isgrounded)
        {
            animator.SetBool("taPulando",false);
        }


        if (flipX == false && movePlayer < 0)
        {
            Flip();
        }
        else if(flipX == true && movePlayer > 0)
        {
            Flip();
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            if (canShield)
            {
                StartCoroutine("Shield");
            }
        }

        if (Input.anyKeyDown && isDead)
        {
            SceneManager.LoadScene(7);
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
    
    IEnumerator Shield()
    {
        canShield = false;
        isShielding = true;
        shieldObject.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        isShielding = false;
        shieldObject.SetActive(false);
        yield return new WaitForSeconds(shieldCooldown);
        canShield = true;


    }

    private void Flip()
    {
        flipX = !flipX;
        float x = transform.localScale.x;

        x *= -1;
        transform.localScale = new Vector3( x, transform.localScale.y, transform.localScale.z);
    }
    

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        //Detectando se o player está na TAG "chao"
        if (col.gameObject.CompareTag("chao"))
        {
            isgrounded = true;
        }

        if (col.gameObject.CompareTag("enemy"))
        {
            if(!isShielding)
                Dano();
            else
            {
                if ((!flipX && col.contacts[0].point.x > transform.position.x)||(flipX && col.contacts[0].point.x < transform.position.x))
                {
                    // manda o inimigo ir pra tras ou destroi, enfim faz algo pq o player defendeu
                }
                else
                {
                    // bateu atras
                    Dano();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       
        if (col.gameObject.CompareTag("espinho"))
        {
            Dano();
        }
        if (col.gameObject.tag == "core")
        {
            Destroy(col.gameObject);
            gcPlayer.core++;
            gcPlayer.coreText.text = gcPlayer.core.ToString();
        }
        
    }

  


    private void Dano()
    {
        vida -= 1;    //tirando dano do player

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
            vidaOn3.enabled = true;
            vidaOff3.enabled = false;
            
            vidaOn2.enabled = true;
            vidaOff2.enabled = false;

            vidaOn.enabled = true;
            vidaOff.enabled = false;

            //toca animacao de morte
            //Time.timeScale = 0f;
            
            isDead = true;

        }
        
        
        
        
        
    }
    
}
