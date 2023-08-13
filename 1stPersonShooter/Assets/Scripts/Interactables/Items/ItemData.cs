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
  public EItemClass ItemClass;
}
