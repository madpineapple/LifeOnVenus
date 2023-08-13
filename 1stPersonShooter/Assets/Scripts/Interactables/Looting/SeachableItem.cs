using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeachableItem : Interactable
{
  private bool looting;
  [SerializeField]
  private GameObject player;
  protected override void Interact()
  {
    looting = !looting;
    player.GetComponent<InventoryInteraction>().InventoryScreenOpened();
  }
}
