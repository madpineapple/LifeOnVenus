using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
  //Hipfire Recoil
  public float recoilX;
  public float recoilY;
  public float recoilZ;

  //Aim Recoil
   public float aimRecoilX;
   public float aimRecoilY;
   public float aimRecoilZ;

  //Settings
  public float snappiness;
  public float returnSpeed;

  public GameObject bullet;

  public float timeBetweenShots;
  public float fireTime;
  public float spread;
  public float reloadTime;
  public int magSize;
  public int bulletsPerTap;
  public bool fullAuto;
  public int spareBullets;
  public string weaponClass;


}
