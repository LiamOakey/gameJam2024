using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerShooting : MonoBehaviour
{
    public TextMeshProUGUI ammoText;

    /* These variables will hold the information of the currently equiped weapon. To swap weapons the variables will be switched to the data of another weapon*/
    static float fireRate; //how many times the character will shoot per second
    int mag; //Max Ammo
    int currentAmmo; 
    int projectileCount; // Amount of projectiles fired per shot
    float reloadSpeed; // time in seconds
    float spread; //Random variation on the y axis for shots
    public GameObject currentProjectile; // The projectile prefab to be spawned

    public bool canFire = true;
    bool canReload = true;
    public Transform firePoint; // The position where the projectile will be spawned


    /*Below is a list of gunData and a projectile for each weapon. Gundata describes variables like shooting rate and spread, projectile stores damage and speed.*/
    public GunData pistol; //Gun that the character is using
    public GameObject pistolProjectile;

    public GunData minigun;
    public GameObject minigunProjectile;

    private Camera mainCamera; // The main camera in the scene

    void Awake()
    { 
        currentProjectile = pistolProjectile;
        fireRate = pistol.fireRate;
        fireRate = 1/fireRate;
        mainCamera = Camera.main; // Get the main camera in the scene
        mag = pistol.mag;
        currentAmmo = mag;
        reloadSpeed = pistol.reloadSpeed;
        projectileCount = pistol.projectileCount;
        spread = pistol.spread; 
        ammoText.text = mag.ToString(); //set up ammo counter

        Invoke("switchToMinigun",5f);
    }

    void Update()
    {
        shootingHandler();

        reloadHandler();
    }

    //Shooting handler will check if player can fire
   void shootingHandler(){
    if (Input.GetMouseButton(0) && canFire && canReload) // Check if the fire button (left-click) is being pressed/held
        {

            if(currentAmmo>0){
                Fire(); // Call the Shoot method to spawn a projectile
                canFire = false;
                Invoke("shotReset", fireRate);
            }else{
                if(canReload){
                    reload();
                }
            }
            
        }
   }

    void reloadHandler(){
        if(Input.GetKeyDown("r") && canReload){
            reload();
        }
    }

    //Fire will be called when mouse is clicked, to manage how many time shoot will be called
    void Fire(){
        currentAmmo--;
        ammoText.text = currentAmmo.ToString();//update ammo counter
        for(int i=0; i<projectileCount;i++){
            Shoot();
        }
    }

    void Shoot()
    {
        
        GameObject projectile; //Bullet
        float bulletSpeed = currentProjectile.GetComponent<Bullet>().speed;

        Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates
        
        mousePosition.z = -mainCamera.transform.position.z; // Set the z-coordinate of the mouse position to be the same as the camera's z-coordinate

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition); // Convert the screen coordinates to world coordinates

        Vector2 direction = (worldPosition - firePoint.position).normalized; // Calculate the direction from the fire point to the mouse position
        
        /*Shot spread is based on the weapons "spread" value, spread works as a displacement
         of the mouse on the x and y axis, with the distance being a random number between 
        the positive and negative spread value */

        float shotSpread = Random.Range(-spread,spread);
        direction.x += shotSpread;
        shotSpread = Random.Range(-spread,spread);
        direction.y += shotSpread;
        


        // Spawn a new projectile at the fire point and rotates
        projectile = Instantiate(currentProjectile, firePoint.position, Quaternion.Euler(0,0,gunRotation.angle));


        projectile.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    void shotReset(){
        canFire = true;
    }

    void reload(){
        canReload = false;
        canFire = false;
        Invoke("load",reloadSpeed);
        ammoText.text = "0";
    }

    void load(){
        canReload = true;
        currentAmmo = mag;
        canFire = true;
        ammoText.text = currentAmmo.ToString(); //update ammo counter
    }


    void switchToMinigun(){
        currentProjectile = minigunProjectile;
        fireRate = minigun.fireRate;
        fireRate = 1/minigun.fireRate;
        mag = minigun.mag;
        currentAmmo = minigun.mag;
        reloadSpeed = minigun.reloadSpeed;
        projectileCount = minigun.projectileCount;
        spread = minigun.spread;
        ammoText.text = minigun.mag.ToString(); //set up ammo counter
    }


}
