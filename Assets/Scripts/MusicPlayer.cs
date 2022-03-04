using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void SetupSingleton(){
        if (FindObjectsOfType(GetType()).Length > 1){
            Destroy(gameObject);
        } else{
            DontDestroyOnLoad(gameObject);
        }
    }
    void Awake(){
        SetupSingleton();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
