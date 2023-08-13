using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  public Camera cam;
  private Transform attackPoint;
  private InputManager inputManager;
  [SerializeField]
  private GameObject WeaponHolder;
  public GameObject equippedWeapon;
  private GameObject bullet;
  private AudioSource gunShot;
  private bool gunShooting;
  
  //bullet force
  public float shootForce;
  public float upwardForce;

  //gun stats
  private float timeBetweenShots;
  public float fireTime;
  private float reloadTime;
  private int magSize;
  private int maxAmmo;
  private int bulletsPerTap;
  private bool fullAuto;
  private string gunClass;

  int bulletsLeftInMag;
  int bulletsShot;
  
  public bool readyToShoot, reloading;

  //Graphics
  [SerializeField]
  public GameObject muzzleFlash;
  public TextMeshProUGUI ammunitionDisplay;

  private Recoil recoil;
  private void Awake()
  {
    readyToShoot = true;
  }
  // Start is called before the first frame update
  void Start()
    {
    inputManager = GetComponent<InputManager>();
    ammunitionDisplay.SetText("");
    recoil = WeaponHolder.transform.GetComponent<Recoil>();
  }

  // Update is called once per frame
  void Update()
    {

    if (WeaponHolder.transform.childCount > 0)
    {
      equippedWeapon = WeaponHolder.transform.GetChild(0).gameObject;

      if (equippedWeapon.gameObject.tag == "melee")
        Debug.Log("Whack!");
      if (equippedWeapon.gameObject.tag == "gun")
      {
        magSize = equippedWeapon.GetComponent<FireArm>().magSize;
        reloadTime = equippedWeapon.GetComponent<FireArm>().reloadTime;
        bulletsPerTap = equippedWeapon.GetComponent<FireArm>().bulletsPerTap;
        timeBetweenShots = equippedWeapon.GetComponent<FireArm>().timeBetweenShots;
        fullAuto = equippedWeapon.GetComponent<FireArm>().fullAuto;
        attackPoint = equippedWeapon.transform.GetChild(0).transform;
        bullet = equippedWeapon.GetComponent<FireArm>().bullet;
        gunShot = equippedWeapon.GetComponent<AudioSource>();
        gunClass = equippedWeapon.GetComponent<FireArm>().weaponClass;
        maxAmmo = equippedWeapon.GetComponent<FireArm>().spareBullets;

        if (ammunitionDisplay != null)
          ammunitionDisplay.SetText(bulletsLeftInMag / bulletsPerTap + "/" + maxAmmo / bulletsPerTap);

        if (inputManager.onFoot.Reload.triggered && bulletsLeftInMag < magSize && !reloading && maxAmmo > 0)
          Reload();

        if (readyToShoot && !reloading && bulletsLeftInMag <= 0 && maxAmmo > 0)
          Reload();
        if (inputManager.onFoot.Attack.triggered && bulletsLeftInMag > 0 && readyToShoot)
        {
          bulletsShot = 0;
          Shoot();
          gunShooting = !gunShooting;
          gunShot.Play();
        }
        if (inputManager.onFoot.AttackFullAuto.inProgress && fullAuto && fireTime <= 0 && bulletsLeftInMag > 0)
        {
          bulletsShot = 0;
          Shoot();
          gunShot.Play();

          fireTime = timeBetweenShots;
        }
        if (fireTime > 0)
        {
          fireTime -= Time.deltaTime;
        }
      }
    }
  }

  void Shoot()
  {
    StartCoroutine(FireRate());
    //Find the exact hit using raycast
    Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

    //check if ray hits something
    Vector3 targetPoint;
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
      targetPoint = hit.point;
    }
    else
      targetPoint = ray.GetPoint(75);

    attackPoint.transform.LookAt(targetPoint);

    //calculate direction from attack point to targetPoint;
    Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

    //Instantiate bullet/projectile
    GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
    currentBullet.transform.forward = directionWithoutSpread.normalized;
    //Add forces to bullet
    currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);
    currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

    recoil.RecoilFire();

    //instantiate muzzleflash
    if (muzzleFlash != null)
      Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

    bulletsLeftInMag--;
    bulletsShot++;

    //if (bulletsShot < bulletsPerTap && bulletsLeftInMag > 0)
    //  Invoke(nameof(Shoot), timeBetweenShots);

     ResetShot();

  }

  IEnumerator FireRate()
  {
    readyToShoot = false;

    yield return new WaitForSeconds(timeBetweenShots);
    readyToShoot = true;
  }
  private void ResetShot()
  {
    if(gunClass == "shotgun")
    {
      equippedWeapon.GetComponent<AudioSource>().Play();
    }
  }

  private void Reload()
  {
    reloading = true;
    Invoke(nameof(ReloadFinished), reloadTime);
  }
  private void ReloadFinished()
  {
    int ammoNeeded = magSize - bulletsLeftInMag;
    maxAmmo -= ammoNeeded;
    bulletsLeftInMag = magSize;
    equippedWeapon.GetComponent<FireArm>().spareBullets = maxAmmo;
    reloading = false;
  }
}
