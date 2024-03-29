using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

//using Scene = UnityEditor.SearchService.Scene;

public class PlayerController : MonoBehaviour
{
    public int vida;
    private int vidaMaxima = 3;

    [SerializeField] private Image vidaOn;
    [SerializeField] private Image vidaOff;

    [SerializeField] private Image vidaOn2;
    [SerializeField] private Image vidaOff2;

    [SerializeField] private Image vidaOn3;
    [SerializeField] private Image vidaOff3;


    [SerializeField] private GameObject shieldObject;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> sounds;
    
    
    public Text coreTxt;
    private int score;

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
    public GameObject dashObjTime; // será o objeto do tempo que aparece na tela de hub

    [SerializeField] private TrailRenderer tr;


    //Sistema de Defesa
    private bool canShield = true;//Posso usar o escudo?
    private bool isShielding; // escudo ativo.
    public float shieldTime = 2f;// Responsavel pelo tempo do escudo
    public float shieldCooldown = 0.5f;//Responsavel pelo controle do tempo de espera ate pode usar o escudo dnv
    public GameObject shieldObjTime;// será o objeto do tempo que aparece na tela de hub

    public float yOffset, raySize, jumpAnimMultiplier, xOffset, checkGroundX;

    private float movePlayer; // Sera responsavel pelo INPUT do player

    private bool isDead = false; //Está Morto?

    private bool isInKnockDown;

    private SpriteRenderer playerSpriteRenderer; //Sprite fica piscando
    
    private float damageTime = 1f;


    private float lastSpeedY;
    
    public LayerMask groundLayer;
    void Start()
    {
        vida = vidaMaxima;
        animator = GetComponent<Animator>();
        score = 0;
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(6,3, false);
        if(GameManager.Instance.GetCheckpointAtual() != Vector3.zero)
            transform.position = GameManager.Instance.GetCheckpointAtual();
        
    }

    void FixedUpdate()
    {
        if (isDashing == true)
        {
            return;
        }
        
        if (isDead) return;
        if(!isInKnockDown)
            playerRb.velocity = new Vector2(movePlayer * speed, playerRb.velocity.y); //Movimentando o player, para um lado e para o outro... tanto para a esquerda, quanto para a direita.

       
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + Vector3.down * yOffset, Vector3.down * raySize);
        Debug.DrawRay(transform.position + Vector3.down * yOffset + Vector3.right * xOffset, Vector3.down * raySize);
        Debug.DrawRay(transform.position + Vector3.down * yOffset - Vector3.right * xOffset, Vector3.down * raySize);
    }

    void Update()
    {
    
        
    
        isgrounded = (Physics2D.Linecast(transform.position + Vector3.down * yOffset,
                          transform.position + Vector3.down * (yOffset + raySize), groundLayer) ||
                      Physics2D.Linecast(transform.position + Vector3.down * yOffset + Vector3.right * xOffset,
                          transform.position + Vector3.down * (yOffset + raySize) + Vector3.right * xOffset,
                          groundLayer) ||
                      Physics2D.Linecast(transform.position + Vector3.down * yOffset - Vector3.right * xOffset,
                          transform.position + Vector3.down * (yOffset + raySize) - Vector3.right * xOffset,
                          groundLayer));
        
        if (isDead) return;
        
        coreTxt.text = score.ToString();
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
        
        if (Input.GetKeyDown(KeyCode.X) && canDash == true)
        {
            
            if (!isDashing)
            {
                audioSource.PlayOneShot(sounds[1], 0.3f);
                StartCoroutine("Dash");
                dashObjTime.SetActive(true);
                Invoke("HideDashObject", dashingCoolDown);
            }
           
        }
        //Condiçao do pulo
        if (pulo == true && isgrounded == true)
        {
            audioSource.PlayOneShot(sounds[0]);
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            playerRb.AddForce(new Vector2(0, jumpForce));
            isgrounded = false;
            animator.SetBool("taPulando",true);
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


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (canShield)
            {
                audioSource.PlayOneShot(sounds[3], 0.3f);
                StartCoroutine("Shield");
                
                
            }
        }
        

        
        lastSpeedY = Mathf.Lerp(lastSpeedY, playerRb.velocity.y, Time.deltaTime * jumpAnimMultiplier);
        animator.SetBool("isGrounded", isgrounded);
        animator.SetFloat("SpeedY", lastSpeedY);
    }
    
    IEnumerator Dash()
    {
        animator.SetBool("dashAtivo", true);
        canDash = false;
        isDashing = true;
        float originalGravity = playerRb.gravityScale;
        playerRb.gravityScale = 0f;
        playerRb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        playerRb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("dashAtivo", false);
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }
    
    void HideDashObject()
    {
        dashObjTime.SetActive(false);
    }
    void HideShieldObject()
    {
        shieldObjTime.SetActive(false);
    }
    
    
    IEnumerator Shield()
    {
        canShield = false;
        isShielding = true;
        shieldObject.SetActive(true);
        yield return new WaitForSeconds(shieldTime);
        isShielding = false;
        shieldObject.SetActive(false);
        shieldObjTime.SetActive(true);
        Invoke("HideShieldObject", shieldCooldown);
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


    private void OnCollisionExit2D(Collision2D col)
    {
       /* if (col.gameObject.CompareTag("chao"))
        {
            isgrounded = false;
        }*/
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Detectando se o player está na TAG "chao"
        /*if (col.gameObject.CompareTag("chao") && col.GetContact(0).point.y < transform.position.y)
        {
            isgrounded = true;
        }*/

        if (col.gameObject.CompareTag("enemy"))
        {
            if(!isShielding)
            { 
                if (col.transform.position.x > transform.position.x)
                    Dano(false);
                else
                    Dano(true);
                
            }
            else
            {
                if ((!flipX && col.contacts[0].point.x > transform.position.x)||(flipX && col.contacts[0].point.x < transform.position.x))
                {
                    // manda o inimigo ir pra tras ou destroi, enfim faz algo pq o player defendeu
                }
                else
                {
                    if (col.transform.position.x > transform.position.x)
                        Dano(false);
                    else
                        Dano(true);
                }
            }
        }

        if (col.gameObject.CompareTag("Life"))
        {
            Destroy(col.gameObject);
            vida++;
            if (vida == 3)
            {
                vidaOn2.enabled = false;
                vidaOff2.enabled = true;
            }
            
            if (vida == 2)
            {
                vidaOn.enabled = false;
                vidaOff.enabled = true;
            }

            if (vida >= 4)
            {
                Destroy(col.gameObject);
                vida--;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("MorteInst"))
        {
            Dano(true);
            playerRb.transform.position = GameManager.Instance.GetCheckpointAtual();

        }

        if (col.gameObject.CompareTag("core"))
        {
            score = score + 1;
            Destroy(col.gameObject);
        }
        
        if (col.gameObject.CompareTag("Checkpoint"))
        {
            PlayerPrefs.SetString("ultimaFase",SceneManager.GetActiveScene().name);
            GameManager.Instance.AtualizarFaseAtual(SceneManager.GetActiveScene().name);
            GameManager.Instance.AtualizaCheckpoint(col.transform.position);
            Destroy(col.gameObject);
        }
        
    }

    
    private void Dano(bool left)
    {
        if (isInKnockDown) return;
        
        vida -= 1;    //tirando dano do player

        StartCoroutine(DoKnockDown(left));
        
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

            canDash = false;
            canShield = false;
            Physics2D.IgnoreLayerCollision(6,3);
            StartCoroutine(CallGameOver());

        }
        
        
        
        
        
    }
    
    private IEnumerator DoKnockDown(bool left)
    {
        isInKnockDown = true;
       
        if (left)
        {
            playerRb.AddForce(Vector2.right * 7f, ForceMode2D.Impulse);
        }
        else
        {
            playerRb.AddForce(Vector2.left * 7f, ForceMode2D.Impulse);
        }
        
        Physics2D.IgnoreLayerCollision(6,3);
        for (float i = 0; i < damageTime; i += 0.4f)
        {
            //isInKnockDown = true;
            canDash = false;
            canShield = false;
            playerSpriteRenderer.enabled = false;
            playerSpriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.1f);
            playerSpriteRenderer.enabled = true;
            playerSpriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.1f);
            playerSpriteRenderer.enabled = false;
            playerSpriteRenderer.color = new Color(1f, 0f, 0f, 1f);
            yield return new WaitForSeconds(0.1f);
            playerSpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            
        }
        
        Physics2D.IgnoreLayerCollision(6,3, false);
        yield return new WaitForSeconds(0.05f);
        canDash = true;
        canShield = true;
        isInKnockDown = false;
    }

    private IEnumerator CallGameOver()
    {
        PlayerPrefs.SetString("ultimaFase",SceneManager.GetActiveScene().name);
        GameManager.Instance.AtualizarFaseAtual(SceneManager.GetActiveScene().name);
        playerRb.velocity = Vector2.zero;
        //playerRb.gravityScale = 1f;
        animator.SetBool("MorteAsh",true);
        animator.Play("EstaMorto");
        isDead = true;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(7);
    }

    public void PlayFootSound()
    {
        audioSource.PlayOneShot(sounds[2], 0.5f + Random.value * 0.3f);
    }
}
