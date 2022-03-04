using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    playerScript player;
    void Start(){
        healthText = gameObject.GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<playerScript>();
    }
    void Update(){
        healthText.text = player.GetHealth().ToString();
    }
}
