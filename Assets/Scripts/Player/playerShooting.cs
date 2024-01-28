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
    //Cat launcher
    public GunData catLauncherData;
    public GameObject catLauncherProjectile;
    public GameObject catLauncher;

    //Chicken
    public GunData chickenData;
    public GameObject chickenProjectile;
    public GameObject chicken;

    //Pew
    public GunData pewData;
    public GameObject pewProjectile;
    public GameObject pew;

    //Banana
    public GunData bananaData;
    public GameObject bananaProjectile;
    public GameObject banana;

    //WaterGun
    public GunData waterGunData;
    public GameObject waterGunProjectile;
    public GameObject waterGun;


    //Audio sources
    public AudioSource catLauncher1;
    public AudioSource catLauncher2;

    public AudioSource chicken1;
    public AudioSource chicken2;

    public AudioSource pew1;

    public AudioSource waterGun1;

    public AudioSource banana1;

    

    private Camera mainCamera; // The main camera in the scene
    private Transform transform;

    void Awake()
    { 
        mainCamera = Camera.main; // Get the main camera in the scene
        transform = this.gameObject.GetComponent<Transform>(); 

        switchToPew();
       
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

        projectile.GetComponent<Rigidbody2D>().velocity = (direction * bulletSpeed).normalized * bulletSpeed;


        //Handles audio based on equipped weapon
        playAudio();
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

    //Method will check which weapon the player has and play a sound
    void playAudio(){
        //Cat Launcher
        if(currentProjectile == catLauncherProjectile){
            int sound = (int)Random.Range(0,2);
            Debug.Log(sound);
            if(sound==0){
                catLauncher1.Play();
            }else{
                catLauncher2.Play();
            }
        }else

        //Chicken
        if(currentProjectile == chickenProjectile){
            int sound = (int)Random.Range(0,2);
            if(sound==0){
                chicken1.Play();
            }else{
                chicken2.Play();
            }
        } else

        if(currentProjectile == pewProjectile){
            pew1.Play();
        }else

        if(currentProjectile == bananaProjectile){
            banana1.Play();
        }

        if(currentProjectile == waterGunProjectile){
            waterGun1.Play();
        }

    }

    //Below are the weapon switch statements
    //Inputs follow as (weapon object, weapon data, weapon projectile)

    public void switchToCatLauncher(){
        switchWeapon(catLauncher, catLauncherData, catLauncherProjectile);
    }

    public void switchToChicken(){
        switchWeapon(chicken, chickenData, chickenProjectile);
    }

    public void switchToPew(){
        switchWeapon(pew,pewData,pewProjectile);
    }

    public void switchToBanana(){
        switchWeapon(banana,bananaData,bananaProjectile);
    }

    public void switchToWaterGun(){
        switchWeapon(waterGun,waterGunData,waterGunProjectile);
    }
}
