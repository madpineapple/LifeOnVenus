using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
  [SerializeField]
  private GameObject Player;
  private GameObject equippedWeapon;

  //Rotations
  private Vector3 currentRotation;
  private Vector3 targetRotation;

  //Hipfire Recoil
   private float recoilX;
   private float recoilY;
   private float recoilZ;

  //Aim Recoil
  private float aimRecoilX;
   private float aimRecoilY;
  private float aimRecoilZ;

  //Settings
   private float snappiness;
   private float returnSpeed;

  private bool aimIsActive;

  // Update is called once per frame
  void Update()
    {
    equippedWeapon = Player.GetComponent<PlayerAttack>().equippedWeapon;
    aimIsActive = Player.GetComponent<PlayerAim>().aimIsActive;

    recoilX = equippedWeapon.GetComponent<FireArm>().recoilX;
    recoilY = equippedWeapon.GetComponent<FireArm>().recoilY;
    recoilZ = equippedWeapon.GetComponent<FireArm>().recoilZ;

    aimRecoilX = equippedWeapon.GetComponent<FireArm>().aimRecoilX;
    aimRecoilY = equippedWeapon.GetComponent<FireArm>().aimRecoilY;
    aimRecoilZ = equippedWeapon.GetComponent<FireArm>().aimRecoilZ;

    snappiness = equippedWeapon.GetComponent<FireArm>().snappiness;
    returnSpeed = equippedWeapon.GetComponent <FireArm>().returnSpeed;
    targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed *Time.deltaTime);
    currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
    transform.localRotation = Quaternion.Euler(currentRotation);
    }

  public void RecoilFire()
  {

    if (aimIsActive)
    {
      targetRotation += new Vector3(aimRecoilX, Random.Range(-aimRecoilY, aimRecoilY), Random.Range(-aimRecoilZ, aimRecoilZ));
    }
    else
    {
      targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
  }
}
