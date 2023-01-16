using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PortaPuzzle : MonoBehaviour
{
    public float speed = 3;
    public Transform[] pos; 
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Botao.pisou == true || transform.position.y <= pos[1].position.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if(Botao.pisou == false || transform.position.y >= pos[0].position.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        
    }

    
}
