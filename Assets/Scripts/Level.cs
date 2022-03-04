using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    GameSession gameSession;
    void Start(){
        gameSession = FindObjectOfType<GameSession>();
    }
    public void LoadStartScene(){
        SceneManager.LoadScene(0);
    }
    public void LoadMainLoop(){
        if (gameSession) { gameSession.ResetGame(); }
        SceneManager.LoadScene("MainLoop");
    }
    public void LoadGameOver(){
        SceneManager.LoadScene("GameOver");
    }
    public void QuitGame(){
        Application.Quit();
    }
}