using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextFase : MonoBehaviour
{
   [SerializeField] private string nomeProximaFase;

   private void OnTriggerEnter2D(Collider2D col)
   {
      IrProximaFase();
   }


   private void IrProximaFase()
   {
      SceneManager.LoadScene(nomeProximaFase);
      
   }
}
