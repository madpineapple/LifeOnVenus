using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
  [SerializeField]
  private GameObject weapon;
  private bool isGrabbed;
  [SerializeField]
  private GameObject WeaponHolder;
  [SerializeField]
  private string weapontype;
  private InventoryList inventoryList;
    // Start is called before the first frame update
    void Start()
    {
    inventoryList = new InventoryList();
    }



  protected override void Interact()
  {
    if(WeaponHolder.transform.childCount == 0 )
    {
      isGrabbed = !isGrabbed;
      weapon.GetComponent<Rigidbody>().isKinematic = isGrabbed;
      weapon.transform.position = WeaponHolder.transform.position;
      weapon.transform.rotation = WeaponHolder.transform.rotation;
      weapon.transform.SetParent(WeaponHolder.transform);
    }
    
    else {
      //inventoryList.AddItem(weapon.name);
      Destroy(weapon);
    }

  }
}
