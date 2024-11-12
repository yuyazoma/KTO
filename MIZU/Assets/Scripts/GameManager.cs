using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("selectedScene");
    }

    public void GoToStage1()
    {
        SceneManager.LoadScene("stage gimic");
    }

    public void RetrunTitle()
    {
        SceneManager.LoadScene("titleScene");
    }
}