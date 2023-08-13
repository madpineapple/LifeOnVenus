using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LootingController 
{
  VisualTreeAsset ListEntryTemplate;

  ListView ItemList;
  Label CharClassLabel;
  Label CharNameLabel;



  public void InitializeItemLists(VisualElement root, VisualTreeAsset listElementTemplate)
  {
    EnumerateAllItems();

    ListEntryTemplate = listElementTemplate;

    ItemList = root.Q<ListView>("item-list");


    FillItemList();

    ItemList.selectionChanged += OnItemSelected;

  }

  List<ItemData> AllItems;


  void EnumerateAllItems()
  {
    AllItems = new List<ItemData>();
    AllItems.AddRange(Resources.LoadAll<ItemData>("Items"));
  }

  void FillItemList()
  {
    // Set up a make item function for a list entry
    ItemList.makeItem = () =>
    {
      // Instantiate the UXML template for the entry
      var newListEntry = ListEntryTemplate.Instantiate();

      // Instantiate a controller for the data
      var newListEntryLogic = new InventoryListEntryController();

      // Assign the controller script to the visual element
      newListEntry.userData = newListEntryLogic;

      // Initialize the controller script
      newListEntryLogic.SetVisualElement(newListEntry);

      // Return the root of the instantiated visual tree
      return newListEntry;
    };

    // Set up bind function for a specific list entry
    ItemList.bindItem = (item, index) =>
    {
      (item.userData as InventoryListEntryController).SetItemData(AllItems[index]);
    };

    // Set a fixed item height
    ItemList.fixedItemHeight = 45;

    // Set the actual item's source list/array
    ItemList.itemsSource = AllItems;
  }

  void OnItemSelected(IEnumerable<object> selectedItems)
  {

    // Get the currently selected item directly from the ListView
    var selectedItem = ItemList.selectedItem as ItemData;
    Debug.Log(selectedItem);
    // Handle none-selection (Escape to deselect everything)
    if (selectedItem == null)
    {
      // Clear
      CharClassLabel.text = "";
      CharNameLabel.text = "";

      return;
    }

    // Fill in character details
    CharClassLabel.text = selectedItem.ItemClass.ToString();
    CharNameLabel.text = selectedItem.ItemName;
  }
}
