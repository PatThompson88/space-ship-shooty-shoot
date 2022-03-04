using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    IEnumerator Start(){
        do{
            yield return StartCoroutine(SpawnAllWaves());
        }
        while(looping);
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){
        for(int i = 0; i < waveConfig.GetNumberOfEnemies(); i++){
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathFinder>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnAllWaves(){
        for(int i = startingWave; i < waveConfigs.Count; i++){
            StartCoroutine(SpawnAllEnemiesInWave(waveConfigs[i]));
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
