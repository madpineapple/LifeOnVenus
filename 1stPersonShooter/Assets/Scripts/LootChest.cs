using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : MonoBehaviour
{
  [SerializeField] private LootChestScriptable lootChestScriptable;

  private void Start()
  {
    Debug.Log(lootChestScriptable); ;
  }
}
