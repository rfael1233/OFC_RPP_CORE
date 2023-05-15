using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevel;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    public GameObject botaoConti;

    private void Start()
    {
        if (PlayerPrefs.HasKey("ultimaFase"))
        {
           botaoConti.SetActive(true); 
        }
    }

    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoLevel);
        Vector3 posicaoInicial = new Vector3(-168.88f, -44f, 0f);
        GameManager.Instance.AtualizaCheckpoint(posicaoInicial);
    }

    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        Debug.Log("SAIR DO JOGO");
        Application.Quit();
    }

    public void Continuar()
    {
        GameManager.Instance.CarregaUltimaFase();
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ContinuarMenu()
    {
        string faseAtual = PlayerPrefs.GetString("ultimaFase");
        SceneManager.LoadSceneAsync(faseAtual);
    }
}
