using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType 
{
    Collectible,
    Lethal
}

[CreateAssetMenu(menuName = "SpinWheel/ItemType")]
public class SlotItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
}
