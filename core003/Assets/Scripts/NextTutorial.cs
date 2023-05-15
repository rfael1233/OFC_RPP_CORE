using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTutorial : MonoBehaviour
{
    [SerializeField] private string nomeProximaFase;

    private void OnTriggerEnter2D(Collider2D col)
    {
      
        if (col.gameObject.CompareTag("Player"))
        {
            IrProximaFase1();
        }
      
    }


    private void IrProximaFase1()
    {
     
        SceneManager.LoadScene(nomeProximaFase);
        Vector3 posicaoInicial = new Vector3(-120.97f, -43.61f, 0f);
        GameManager.Instance.AtualizaCheckpoint(posicaoInicial);
      
    }
    
}
