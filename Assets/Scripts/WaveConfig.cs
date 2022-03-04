using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.8f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab(){
        return enemyPrefab;
    }
    public List<Transform> GetWaypoints(){
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform){
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float GetMoveSpeed(){
        return moveSpeed;
    }

    public GameObject GetPathPrefab(){
        return pathPrefab;
    }

    public float GetTimeBetweenSpawns(){ return timeBetweenSpawns; }
    public int GetNumberOfEnemies(){ return numberOfEnemies; }
}
