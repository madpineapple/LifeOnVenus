using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponEquip : MonoBehaviour
{
  private InputManager inputManager;
  public GameObject primaryPrefab;
  public GameObject secondaryPrefab;
  private GameObject currentlyEquippedWepaon;

  public Transform parent;
  private void Start()
  {
    inputManager = GetComponent<InputManager>();
    setPrimaryWeaponAsEquipped();
  }

  private void Update()
  {
    if (inputManager.onFoot.SwitchToPrimaryWeapon.triggered)
    {
      Destroy(currentlyEquippedWepaon);
      setPrimaryWeaponAsEquipped();
    }
    if (inputManager.onFoot.SwitchToSecondaryWeapon.triggered)
    {
      Destroy(currentlyEquippedWepaon);
      setSecondaryWeaponAsEquipped();
    }
  }

  public void setPrimaryWeaponAsEquipped()
  {
    currentlyEquippedWepaon = Instantiate(primaryPrefab, parent.position, parent.rotation, parent);
    currentlyEquippedWepaon.GetComponent<Rigidbody>().isKinematic = true;
  }

  public void setSecondaryWeaponAsEquipped()
  {
     currentlyEquippedWepaon = Instantiate(secondaryPrefab, parent.position, parent.rotation, parent);
    currentlyEquippedWepaon.GetComponent<Rigidbody>().isKinematic = true;
  }
  public void setSecondaryWeaponPrefab(ItemData itemData)
  {
    if(itemData.isEquipped != true){
      secondaryPrefab = null;
    }
    if(itemData.isEquipped == true)
    {
    secondaryPrefab = itemData.objectPrefab;
    }
  }
}
