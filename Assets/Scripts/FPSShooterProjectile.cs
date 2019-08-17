using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSShooterProjectile : MonoBehaviour
{

    private float fireRate;
    private float fireRateTimer = 0f;

    private int damage;

    public Camera gameCam;
    protected float range;
    private Vector3 hitPoint;

    public GameObject fxSpawnPosition;

    private float attackTime;
    public float timeBetweenAttacks = 0.0f;

    public GameObject rifleGunFirePoint;
    public GameObject rifle;

    public GameObject projectile;

    public float rifleFireRate;

    public int totalAmmoInClip;
    public int currentAmmo;
    private bool b_Reloading = false;
    private int reloadingTime;
    public int ammoInBagRifle;
    private int ammoInBag;

    private int currentAmmoRifle;


    public float bulletSpeed;
    //fire rate is used to determine the weapons fire rate
    //1 = 1per sec, 2 = 1 per 2 sec



    // Use this for initialization
    void Start()
    {
        timeBetweenAttacks = rifleFireRate;
      
        rifle.SetActive(true);
        //UpdateWeaponStats();
        reloadingTime = 3;
        currentAmmoRifle = totalAmmoInClip;
        currentAmmo = currentAmmoRifle;
    }

    private void OnEnable()
    {
        b_Reloading = false;
    }

    // Update is called once per frame
    void Update()
    { 
        
        if(b_Reloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            //Debug.Log("Reloading...");
            StartCoroutine(Reload());
            return;
        }

        //fires the selected weapon
        if (Time.time >= attackTime && Input.GetMouseButton(0))
        {
            attackTime = Time.time + timeBetweenAttacks;
            //code to do your attack
            currentAmmo = currentAmmo - 1;

            //rotate the fire point

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 10000))

            {
                    hitPoint = hitInfo.point;  
            }
            else
            {
                hitPoint = gameCam.transform.position + (gameCam.transform.forward * 30);
            }


            Vector3 bulletVector = hitPoint - fxSpawnPosition.transform.position;
            GameObject bulletInstance = Instantiate(projectile, fxSpawnPosition.transform.position, fxSpawnPosition.transform.rotation);
            
            bulletInstance.GetComponent<Rigidbody>().AddForce(bulletVector * bulletSpeed);
            bulletInstance.GetComponent<Projectile>().damage = damage;
            //Debug.Log(go.GetComponent<Projectile>().damage);
        }
    }

    IEnumerator Reload()
    {
        b_Reloading = true;

       
            ammoInBag = ammoInBagRifle;


        
        yield return new WaitForSeconds(reloadingTime);

        if (totalAmmoInClip <= ammoInBag)
        {
            SetAmmoInBag();
            currentAmmoRifle = totalAmmoInClip;
            currentAmmo = currentAmmoRifle;         
        }


        
        b_Reloading = false;
       
    }

    public int SetAmmoInBag()
    {
        ammoInBag = ammoInBag - totalAmmoInClip;

        ammoInBagRifle = ammoInBag;
        
        return (ammoInBag);
    }


    private void UpdateWeaponStats()
    {
                fireRate = rifleFireRate;
                damage = 1;
                range = 400;
                ammoInBagRifle = 20;
                totalAmmoInClip = 5;
                reloadingTime = 3;
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }
}
