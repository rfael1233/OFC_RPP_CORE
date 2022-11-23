using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speedMax;
    public float speed;
    public float forca = 5;
    public bool isGround;
    public bool isDash = false;
    public float tempoDash = 10;
    public float tempoAtual;
    public int mult = 2;
    
    public string ultima = "direita";

    bool flipX;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        

    }

    
    void Update()
    {
        //rb.velocity = transform.right * Time.deltaTime * speed;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, rb.velocity.y);
        

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce(transform.up * forca);
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && isDash == false)
        {
            isDash = true;
            tempoAtual = tempoDash;
        }

        if (isDash)
        {
            //tudo que acontece no dash é aqui
            
            //o tenmpo atual vai se torna o tempo determinado
            speed = speedMax * mult;
            
            tempoAtual -= Time.deltaTime;
            if (tempoAtual <= 0)
            {
                isDash = false;
                speed = speedMax;
            }

        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGround = true;
        }
    }
    
}
