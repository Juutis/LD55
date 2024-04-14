using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{

    [SerializeField]
    private Image imgIcon;
    [SerializeField]
    private TextMeshProUGUI txtCount;
    InventoryItemConfig config;

    public InventoryItemConfig Config { get { return config; } }

    int count = 1;
    public InventoryItemType Type { get { return config.Type; } }

    public void Init(InventoryItemConfig itemConfig)
    {
        config = itemConfig;
        count = itemConfig.Count;
        name = $"{config.Type} #{count}";
        txtCount.text = $"{count}";
        imgIcon.sprite = itemConfig.Sprite;
    }

    public void AddStackableItemCount(int countAddition)
    {
        count += countAddition;
        name = $"{config.Type} #{count}";
        txtCount.text = $"{count}";
    }
    public int Take()
    {
        Debug.Log($"[{name}] taken!");
        count -= 1;
        name = $"{config.Type} #{count}";
        txtCount.text = $"{count}";
        return count;
    }

    public void Drop()
    {
        Debug.Log($"[{name}] dropped!");
    }
}
