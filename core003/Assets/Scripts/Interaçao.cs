using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interaçao : MonoBehaviour
{
    
    public Image imagemReferencia;
    void Start()
    {
        imagemReferencia.enabled = false;
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            imagemReferencia.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagemReferencia.enabled = false;
        }
    }
}
