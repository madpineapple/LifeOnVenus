using UnityEngine;


public enum EItemClass
{
  Gun,
  Melee,
  Health,
  Story,
  Craft
}

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
  public string ItemName;

  public GameObject objectPrefab;
  public bool isEquipped;
  public EItemClass ItemClass;
}
