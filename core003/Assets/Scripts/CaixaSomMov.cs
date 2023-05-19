using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaSomMov : MonoBehaviour
{
    private AudioSource _source;
    private bool emMovimento;
    
    
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            emMovimento = true;
            _source.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            emMovimento = false;
            _source.Stop();
        }
    }
}
