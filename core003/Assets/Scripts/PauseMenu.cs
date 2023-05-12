using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        Vector3 posicaoInicial = new Vector3(-168.88f, -44f, 0f);
        GameManager.Instance.AtualizaCheckpoint(posicaoInicial);
        SceneManager.LoadScene(sceneID);
    }
}
