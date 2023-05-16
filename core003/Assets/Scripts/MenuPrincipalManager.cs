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
    [SerializeField] private GameObject botaoConti;

    private void Start()
    {
        if (PlayerPrefs.HasKey("ultimaFase"))
        {
            Debug.Log("Butão ativo");
            botaoConti.SetActive(true);
        }
    }

    public void Jogar()
    {
        PlayerPrefs.DeleteAll();
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

    //Pause
    public void Continuar()
    {
        GameManager.Instance.CarregaUltimaFase();
    }

    public void VoltarMenu()
    {
        string faseAtual = PlayerPrefs.GetString("ultimaFase");
        SceneManager.LoadSceneAsync(faseAtual);
        SceneManager.LoadScene("Menu");
    }
    
    //Tela Incial
    public void ContinuarMenu()
    {
        string faseAtual = PlayerPrefs.GetString("ultimaFase");
        SceneManager.LoadSceneAsync(faseAtual);
    }
}
