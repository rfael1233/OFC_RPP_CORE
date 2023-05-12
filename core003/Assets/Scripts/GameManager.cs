using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string faseAtual;
    private Vector3 checkpointAtual;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AtualizarFaseAtual(string nomedafase)
    {
        faseAtual = nomedafase;
    }

    public void AtualizaCheckpoint(Vector3 position)
    {
        checkpointAtual = position;
    }
    
    public Vector3 GetCheckpointAtual()
    {
        return checkpointAtual;
    }

    public void CarregaUltimaFase()
    {
        SceneManager.LoadSceneAsync(faseAtual);
    }

    
}
