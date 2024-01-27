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
    public GunData pistolData; //Gun that the character is using
    public GameObject pistolProjectile;

    // Minifun
    public GunData minigunData;
    public GameObject minigunProjectile;

    //Cat launcher
    public GunData catLauncherData;
    public GameObject catLauncherProjectile;
    public GameObject catLauncher;


    private Camera mainCamera; // The main camera in the scene
    private Transform transform;

    void Awake()
    { 
        switchWeaponData(catLauncherData);
        mainCamera = Camera.main; // Get the main camera in the scene
        transform = this.gameObject.GetComponent<Transform>(); 
       
    }

    void Update()
    {
        shootingHandler();

        reloadHandler();
    }

    //Shooting handler will check if player can fire
   void shootingHandler(){
    if(PauseMenu.gameIsPaused) return;

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
        if(PauseMenu.gameIsPaused) return;
        
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
        


        // Spawn a new projectile at the fire point and rotates it
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

    // Switches all current weapon stats to the weapon provided
    void switchWeaponData(GunData gun){
        fireRate = gun.fireRate;
        fireRate = 1/gun.fireRate;
        mag = gun.mag;
        currentAmmo = gun.mag;
        reloadSpeed = gun.reloadSpeed;
        projectileCount = gun.projectileCount;
        spread = gun.spread;
        ammoText.text = gun.mag.ToString(); //set up ammo counter
    }


    //This method switches the active sprite with the new sprite
    void switchWeapon(GameObject newWeapon, GunData weaponData, GameObject projectile){

        switchWeaponData(weaponData);
        currentProjectile = projectile;

        //Deletes all children/sprite
        foreach (Transform child in transform) {
            if(child != null){
	            GameObject.Destroy(child.gameObject);
            }
        }  

        newWeapon = Instantiate(newWeapon, transform.position, Quaternion.identity);
        newWeapon.transform.parent = transform;

        //Sets the new spawn point for projectiles to the new weapon's second child, which should be it's firepoint
        firePoint = newWeapon.transform.GetChild(0).transform.GetChild(0).gameObject.transform;
        
    }


    //Below are the weapon switch statements
    //Inputs follow as (weapon object, weapon data, weapon projectile)

    void switchToMinigun(){
        switchWeapon(catLauncher, minigunData, minigunProjectile);
    }


    void switchToCatLauncher(){
        switchWeapon(catLauncher, catLauncherData, catLauncherProjectile);
    }


}
