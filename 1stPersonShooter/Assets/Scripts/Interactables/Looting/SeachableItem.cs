using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.ComponentModel;
using System.IO;

public class SeachableItem : Interactable
{
  private bool looting;
  private GameObject player;
  [SerializeField]
  private GameObject lootableItem;
  List<ItemData> LootList;
  protected override void Interact()
  {
    looting = !looting;
    player = GameObject.FindGameObjectWithTag("Player");
    LootList= new List<ItemData>();

    LootList.AddRange(lootableItem.GetComponent<InventoryList>().items);
    player.GetComponent<InventoryInteraction>().LootingInventoryInteraction(LootList, lootableItem.name);
  }
}
