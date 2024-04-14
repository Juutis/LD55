using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Configs/InventoryItemConfig", order = 1)]
public class InventoryItemConfig : ScriptableObject
{
    public InventoryItemType Type;
    public Sprite Sprite;
    public int Count = 1;

    public bool IsStackable = false;
    public bool IsHealth = false;
}
