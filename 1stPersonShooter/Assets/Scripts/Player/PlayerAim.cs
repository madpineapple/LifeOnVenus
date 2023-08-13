using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
  private InputManager inputManager;
  [SerializeField]
  private GameObject WeaponHolder;
  [SerializeField]
  private GameObject AimedInWeaponHolder;
  private Vector3 HipFire;
  public bool aimIsActive;

    // Start is called before the first frame update
    void Start()
    {
    HipFire = WeaponHolder.transform.localPosition;
    inputManager = GetComponent<InputManager>();
    aimIsActive = false;
  }

  // Update is called once per frame
  void Update()
    {
        if(inputManager.onFoot.Aim.inProgress)
        {
          aimIsActive = true;
         WeaponHolder.transform.localPosition = AimedInWeaponHolder.transform.localPosition;
    }
    else
    {
      WeaponHolder.transform.localPosition = HipFire;
      aimIsActive = false;
    }

  }
}
