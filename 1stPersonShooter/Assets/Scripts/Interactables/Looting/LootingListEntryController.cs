using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LootingEntryController : MonoBehaviour
{
  Label NameLabel;

  //This function retrieves a reference to the 
  //character name label inside the UI element.

  public void SetVisualElement(VisualElement visualElement)
  {
    NameLabel = visualElement.Q<Label>("item-name");
  }

  //This function receives the character whose name this list 
  //element displays. Since the elements listed 
  //in a `ListView` are pooled and reused, it's necessary to 
  //have a `Set` function to change which character's data to display.

  public void SetLootableListData(ItemData itemData)
  {
    NameLabel.text = itemData.ItemName;
  }
}
