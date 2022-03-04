using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinder : MonoBehaviour
{
    [SerializeField] bool randomSpeed = false;
    [SerializeField] double minSpeedFactor = 0.75;
    [SerializeField] double maxSpeedFactor = 3;
    public double movementSpeed = 1f;
    List<Transform> waypoints;
    WaveConfig waveConfig;
    int waypointIndex = 0;
    System.Random randomGenerator = new System.Random();

    // Generator to create and position waypoints
    // Observe dangers and jump away
    // Maneuver offensively and shoot
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    public void SetWaveConfig(WaveConfig wave){
        this.waveConfig = wave;
        waypoints = this.waveConfig.GetWaypoints();
        movementSpeed = waveConfig.GetMoveSpeed();
    }

    private void handleMove(){
        if(waypointIndex < waypoints.Count){
            var targetPosition = waypoints[waypointIndex].position;
            var movementThisFrame = (float)movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if(transform.position == targetPosition){
                if(randomSpeed){
                    movementSpeed = waveConfig.GetMoveSpeed() * (randomGenerator.NextDouble() * (maxSpeedFactor - minSpeedFactor) + minSpeedFactor);
                }
                waypointIndex++;
            }
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleMove();
    }
}
