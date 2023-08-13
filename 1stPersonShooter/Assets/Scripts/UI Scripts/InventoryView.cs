using UnityEngine;
using UnityEngine.UIElements;

public class InventoryView : MonoBehaviour
{

  [SerializeField]
  VisualTreeAsset ListEntryTemplate;

void OnEnable()
  {
    // The UXML is already instantiated by the UIDocument component
    var uiDocument = GetComponent<UIDocument>();

    // Initialize the character list controller
    var itemListController = new InventoryListController();
    itemListController.InitializeInventory(uiDocument.rootVisualElement, ListEntryTemplate);
  }
}
