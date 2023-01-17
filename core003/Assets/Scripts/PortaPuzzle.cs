using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortaPuzzle : MonoBehaviour
{
    public float speed = 3;
    public Transform[] pos;
    public Botao botao;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (botao.pisou == true || transform.position.y <= pos[1].position.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if(botao.pisou == false || transform.position.y >= pos[0].position.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        
    }

    
}
