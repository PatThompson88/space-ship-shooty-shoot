using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 10;
    float randomSpeedFactor;
    [Header("Weapons")]
    [SerializeField] List<GameObject> weapons;
    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [Header("SFX")]
    [SerializeField] [Range(0,1)] float SFXVolume = 0.75f;
    [SerializeField] AudioClip deathSFX;
    private void Die(){
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, transform.position, SFXVolume);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        Destroy(gameObject);
    }
    private void ProcessHit(DamageDealer damageDealer){
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0){
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){ return; }
        ProcessHit(damageDealer);
    }
    private void Update() {
        foreach (GameObject w in weapons){
            w.GetComponent<Weapon>().CountDownAndShoot(transform);
        }
    }
}
