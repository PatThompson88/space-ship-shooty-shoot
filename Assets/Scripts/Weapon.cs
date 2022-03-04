using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float prjSpeed = 3f;
    [Header("SFX")]
    [SerializeField] [Range(0, 1)] float shotVolume = 0.5f;
    [SerializeField] AudioClip shootSFX;
    float shotCounter;
    void Start(){
        ResetShotCounter();
    }
    void ResetShotCounter(){
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }
    void launchProjectile(GameObject objProjectile, Vector3 objPosition, Quaternion objRotation, Vector2 velocity){
        GameObject prj = Instantiate(objProjectile, objPosition, objRotation);
        prj.GetComponent<Rigidbody2D>().velocity = velocity;
    }
    private void Fire(Vector2 position){
        Vector2 projectileVelocity = gameObject.transform.up*-1*prjSpeed;
        launchProjectile(projectile, position, gameObject.transform.rotation, projectileVelocity);
        AudioSource.PlayClipAtPoint(shootSFX, transform.position, shotVolume);
        ResetShotCounter();
    }
    public void CountDownAndShoot(Transform objTransform){
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f){
            Vector2 projectileVelocity = objTransform.up*-1*prjSpeed;
            launchProjectile(projectile, objTransform.position, objTransform.rotation, projectileVelocity);
            AudioSource.PlayClipAtPoint(shootSFX, transform.position, shotVolume);
            ResetShotCounter();
        }
    }
}
