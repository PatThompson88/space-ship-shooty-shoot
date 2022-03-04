using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour{
    public int score = 0;
    private void SetupSingleton(){
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberOfGameSessions > 1){
            Destroy(gameObject);
        } else{
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Awake(){
        SetupSingleton();
    }
    public int GetScore(){
        return score;
    }
    public void AddToScore(int scoreValue){
        score += scoreValue;
    }
    public void SubtractFromScore(int scoreValue){
        score -= scoreValue;
    }
    public void ResetGame(){
        Destroy(gameObject);
    }
    private void Update() {
        // update score
    }
}
