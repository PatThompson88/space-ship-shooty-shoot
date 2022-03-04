using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class playerScript : MonoBehaviour
{
    
    [Header("Player")]
    private Rigidbody2D rb;
    public Vector3 spawnLocation = new Vector3(0.15f, -6.75f, 2f);
    [SerializeField] int health = 200;
    public int lives = 3;
    public int scoreBounty = 20;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float padding = 0.5f;
    private float movementX = 0f;
    private float movementY = 0f;
    private float xMin;
    private float yMin;
    [Header("Weapons")]
    public GameObject primaryLaser;
    public float attackSpeedPrimary = 5f;  // (attacks/s)
    public float prjSpeed = 15f;
    private bool firingPrimary = false;
    private bool primaryWeaponReady = true;
    [Header("SFX")]
    [SerializeField] [Range(0,1)] float deathVolume = 1f;
    [SerializeField] [Range(0,1)] float shootVolume = 0.25f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip shootSFX;
    [Header("VFX")]
    [SerializeField] GameObject deathVFX;
    [Header("Utils")]
    private GameSession gameSession;
    [SerializeField] GameObject Level;


// INITIALIZE GAMEOBJECT ****************************** INITIALIZE GAMEOBJECT ****************************** INITIALIZE GAMEOBJECT
    void SetMovementLimits(){
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 2)).x + padding;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 2)).y + padding;
    }
    
    void Start(){
        SetMovementLimits();
        rb = GetComponent<Rigidbody2D>();
        gameSession = FindObjectOfType<GameSession>();
    }

// INPUT HANDLING ****************************** INPUT HANDLING ****************************** INPUT HANDLING
    void OnMove(InputValue movementValue){
        Vector2 mov = movementValue.Get<Vector2>();
        movementX = mov.x;
        movementY = mov.y;
    }

    void OnFirePrimary(InputValue fire){
        firingPrimary = fire.Get<float>() > 0;
    }

// OBJECT BEHAVIOR ****************************** OBJECT BEHAVIOR ****************************** OBJECT BEHAVIOR
    public int GetHealth(){
        return health;
    }
    void launchProjectile(GameObject objProjectile, Vector3 objPosition, Quaternion objRotation, Vector2 velocity){
        GameObject prj = Instantiate(objProjectile, objPosition, objRotation);
        prj.GetComponent<Rigidbody2D>().velocity = velocity;
    }
    private void HandleMovement(){
        var newXPos = Mathf.Clamp(transform.position.x + movementX*movementSpeed*Time.deltaTime, xMin, xMin*-1);
        var newYPos = Mathf.Clamp(transform.position.y + movementY*movementSpeed*Time.deltaTime, yMin, yMin*-1);
        transform.position = new Vector3(newXPos, newYPos, transform.position.z);
    }
    IEnumerator FirePrimary(){
        primaryWeaponReady = false;
        Vector2 projectileVelocity = transform.up*prjSpeed;
        launchProjectile(primaryLaser, transform.position, transform.rotation, projectileVelocity);
        AudioSource.PlayClipAtPoint(shootSFX, transform.position, shootVolume);
        yield return new WaitForSeconds(1/attackSpeedPrimary);
        primaryWeaponReady = true;
    }
    private void HandleFirePrimary(){
        if(firingPrimary && primaryWeaponReady){
            StartCoroutine(FirePrimary());
        }
    }
    IEnumerator PlayerDies(){
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        yield return new WaitForSeconds(explosion.GetComponent<ParticleSystem>().main.duration + 0.5f);
        HandleDeath();
    }
    private void HandleDeath(){
        lives -= 1;
        gameSession.SubtractFromScore(scoreBounty);
        if(lives > 0){
            gameObject.transform.position = spawnLocation;
            health = 200;
            gameObject.GetComponent<PlayerInput>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        } else {
            Level.GetComponent<Level>().LoadGameOver();
        }
    }
    private void ProcessHit(DamageDealer damageDealer){
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if(health <= 0){
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponent<PlayerInput>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(PlayerDies());
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer){ return; }
        ProcessHit(damageDealer);
    }
    void FixedUpdate(){
        HandleMovement();
        HandleFirePrimary();
    }
}
