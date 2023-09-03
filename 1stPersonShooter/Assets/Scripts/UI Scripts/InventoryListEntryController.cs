using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;


public class InventoryListEntryController
{
  Label ItemLabel;
  Toggle EquipWeaponToggle;
  GameObject player;
 


  public void SetVisualElement(VisualElement visualElement)
  {
    ItemLabel = visualElement.Q<Label>("item-name");
    EquipWeaponToggle = visualElement.Q<Toggle>("item-toggle");
  }

  public void SetItemData(ItemData itemData) 
  {
    ItemLabel.text = itemData.ItemName;
    EquipWeaponToggle.value = itemData.isEquipped;
    EquipWeaponToggle.RegisterValueChangedCallback((evt) => OnEquipWeaponBoxToggled(EquipWeaponToggle, evt.newValue, itemData));
  }
  void OnEquipWeaponBoxToggled(Toggle changedToggle, bool isChecked, ItemData itemData)
{ 
  itemData.isEquipped = isChecked;
  player = GameObject.FindWithTag("Player");
  player.GetComponent<PlayerWeaponEquip>().setSecondaryWeaponPrefab(itemData);
}
}
