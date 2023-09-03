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

  VisualElement listViewContainer;
  GameObject player;
  GameObject lootedItem;
string lootedItemName;

  public void InitializeItemLists(VisualElement root, VisualTreeAsset listElementTemplate, List<ItemData> itemData, string lootableItemName)
  {
    EnumerateAllLootableItems(itemData);
    listViewContainer = root.Q<VisualElement>("list-view-container");

    ListEntryTemplate = listElementTemplate;

    ListView listView = new();
    ItemList = listView;
    ItemList.name = "item-list";
    listViewContainer.Add(ItemList);


    FillItemList();
    lootedItemName = lootableItemName;
    ItemList.selectionChanged += OnItemSelected;

  }

  List<ItemData> AllItems;
  List<ItemData> PlayerItems;


  void EnumerateAllLootableItems(List<ItemData> lootList)
  {
    AllItems = new List<ItemData>();
    AllItems = lootList;
  }

  void FillItemList()
  {
    
    // Set up a make item function for a list entry
    ItemList.makeItem = () =>
    {
      // Instantiate the UXML template for the entry
      var newListEntry = ListEntryTemplate.Instantiate();

      // Instantiate a controller for the data
      var newListEntryLogic = new LootingListEntryController();

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
      (item.userData as LootingListEntryController).SetLootableListData(AllItems[index]);
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
    player = GameObject.FindWithTag("Player");
    PlayerItems = new List<ItemData>();
    PlayerItems.AddRange(player.GetComponent<InventoryList>().items);
    PlayerItems.Add(selectedItem);
    player.GetComponent<InventoryList>().items = PlayerItems;
    lootedItem = GameObject.Find(lootedItemName);
    AllItems.Remove(selectedItem);
    lootedItem.GetComponent<InventoryList>().items = AllItems;
    UpdateUI();
  }

  public void UpdateUI(){
        listViewContainer.Clear();

    ListView listView = new();
    ItemList = listView;
    ItemList.name = "item-list";
    listViewContainer.Add(ItemList);

    FillItemList();
   ItemList.selectionChanged += OnItemSelected;

  }
}
