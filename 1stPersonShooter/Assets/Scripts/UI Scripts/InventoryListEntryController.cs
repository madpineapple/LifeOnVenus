using UnityEngine.UIElements;

public class InventoryListEntryController
{
  Label ItemLabel;

  public void SetVisualElement(VisualElement visualElement)
  {
    ItemLabel = visualElement.Q<Label>("item-name");
  }

  public void SetItemData(ItemData itemData) 
  {
    ItemLabel.text = itemData.ItemName;
  }
}
