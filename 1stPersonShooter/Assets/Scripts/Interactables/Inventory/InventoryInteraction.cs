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

  public static bool inventoryScreenOpened;

  private UIDocument uiDocument;

  [SerializeField]
  VisualTreeAsset ListEntryTemplate;

  [SerializeField]
  VisualTreeAsset LootingListEntryTemplate;
  void Start()
    {
    inputManager = GetComponent<InputManager>();
    playerAttack = GetComponent<PlayerAttack>();
    inventoryScreen.SetActive(false);
    uiDocument = inventoryScreen.GetComponent<UIDocument>();
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

  public void LootingInventoryInteraction()
  {
    inventoryScreenOpened = !inventoryScreenOpened;
    InventoryScreenOpened();
    var lootingListController = new LootingController();
    lootingListController.InitializeItemLists(uiDocument.rootVisualElement, LootingListEntryTemplate);
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
