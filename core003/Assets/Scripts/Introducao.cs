using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Introducao : MonoBehaviour
{
    [SerializeField] private string nomeProximaScene;
    [SerializeField] private string nextFase;
    [SerializeField] private int numImg = 9;
    public float _timeRotina;
    private int _cont = 0;
    
    void Start()
    {
        StartCoroutine(Rotina());
    }
    public IEnumerator Rotina()
    {
        GameObject img = GameObject.Find("img" + _cont);
        img.GetComponent<RawImage>().enabled = true;
        _cont++;
        
        if (_cont < numImg)
        {
            yield return new WaitForSeconds(_timeRotina);
            StartCoroutine(Rotina());
        }
        else
        {
            yield return new WaitForSeconds(_timeRotina);
            SceneManager.LoadScene(nomeProximaScene);
        }
        
    }

    public void NextFase()
    {
        SceneManager.LoadScene(nextFase);
    }
}
