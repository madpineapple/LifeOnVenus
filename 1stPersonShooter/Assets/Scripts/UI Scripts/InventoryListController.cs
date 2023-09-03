using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryListController
{
  VisualTreeAsset ListEntryTemplate;

  ListView ItemList;
  Label CharClassLabel;
  Label CharNameLabel;
  Button statsButton;
  Button invButton;
  VisualElement contentContainer;
  VisualElement rightContainer;
  VisualElement detailsContainer;
  GameObject player;
  List<ItemData> playerInventoryList;
  //probably shoulld rename this at a later date
  public void InitializeInventory(VisualElement root, VisualTreeAsset listElementTemplate)
  {
    statsButton = root.Q<Button>("stats-button");
    statsButton.clicked += () => TabClicked(statsButton.name, root, listElementTemplate);
    invButton = root.Q<Button>("inventory-button");
    invButton.clicked += () => TabClicked(invButton.name, root, listElementTemplate);

    contentContainer = root.Q<VisualElement>("content-container");
  }

  public void InitializeStats()
  {
    var imageElement = new Image();
    byte[] fileData = File.ReadAllBytes("Assets/PreFabs/beach_photo.png");
    Texture2D texture = new Texture2D(2,2);
    texture.LoadImage(fileData);
    imageElement.image = texture;
    contentContainer.Add(imageElement);
  }
  public void InitializeItemList(VisualElement root, VisualTreeAsset listElementTemplate)
  {
    EnumerateAllItems();

    ListEntryTemplate = listElementTemplate;

    ListView listView = new();
    ItemList = listView;
    ItemList.name = "item-list";
    contentContainer.Add(ItemList);

    //create right container
    rightContainer = new VisualElement();
    rightContainer.name = "right-container";
    contentContainer.Add(rightContainer);

    detailsContainer = new VisualElement();
    detailsContainer.name = "details-container";
    rightContainer.Add(detailsContainer);

    CharClassLabel = new Label("") {
      name = "item-class"
    };
    CharNameLabel = new Label("")
    {
      name = "item-name"
    };
    

    detailsContainer.Add(CharClassLabel);
    detailsContainer.Add(CharNameLabel);
    
    FillItemList();

    ItemList.selectionChanged+= OnItemSelected;
  }

  List<ItemData> AllItems;
  

  void EnumerateAllItems()
  {
    player = GameObject.FindWithTag("Player");
    AllItems = new List<ItemData>();
    AllItems.AddRange(player.GetComponent<InventoryList>().items);
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

  void TabClicked(string buttonName, VisualElement root, VisualTreeAsset listElementTemplate)
  {
    contentContainer.Clear();
    if(buttonName == "inventory-button")
    {
      InitializeItemList(root, listElementTemplate);
    }
    if(buttonName == "stats-button")
    {
      InitializeStats();
    }
  }
}
