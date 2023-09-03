using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class InventoryInteraction : MonoBehaviour
{
  private InputManager inputManager;
  private PlayerAttack playerAttack;
  [SerializeField]
  public GameObject inventoryScreen;

  [SerializeField]
  public GameObject lootInventoryScreen;

  public static bool inventoryScreenOpened;
  public static bool lootInventoryScreenOpened;

  private UIDocument uiDocument;
  private UIDocument lootUIDocument;

  [SerializeField]
  VisualTreeAsset ListEntryTemplate;

  [SerializeField]
  VisualTreeAsset LootingListEntryTemplate;
  void Start()
    {
    inputManager = GetComponent<InputManager>();
    playerAttack = GetComponent<PlayerAttack>();
    inventoryScreen.SetActive(false);
    lootInventoryScreen.SetActive(false);
    uiDocument = inventoryScreen.GetComponent<UIDocument>();
    lootUIDocument = lootInventoryScreen.GetComponent<UIDocument>();
  }

  void Update()
    {
    if (inputManager.onFoot.Inventory.triggered)
    {
      inventoryScreenOpened = !inventoryScreenOpened;
      InventoryScreenOpened();
      // Initialize the character list controller
      var itemListController = new InventoryListController();
      itemListController.InitializeInventory(uiDocument.rootVisualElement, ListEntryTemplate);
    }
    }

  public void LootingInventoryInteraction(List<ItemData> lootList, string lootableItemName)
  {
    lootInventoryScreenOpened = !lootInventoryScreenOpened;
    
    if (lootInventoryScreenOpened)
    {
      lootInventoryScreen.SetActive(true);
       Time.timeScale = 0f;
      playerAttack.readyToShoot = false;
      var lootingListController = new LootingController();
      lootingListController.InitializeItemLists(lootUIDocument.rootVisualElement, ListEntryTemplate, lootList, lootableItemName);
    }
    else
    {
      lootInventoryScreen.SetActive(false);
      Time.timeScale = 1;
      playerAttack.readyToShoot = true;
    }
   
  }

  public void InventoryScreenOpened()
  {
    if (inventoryScreenOpened)
    {
      inventoryScreen.SetActive(true);
      Time.timeScale = 0f;
      playerAttack.readyToShoot = false;
    }
    else
    {
      inventoryScreen.SetActive(false);
      Time.timeScale = 1;
      playerAttack.readyToShoot = true;

    }
  }
}
