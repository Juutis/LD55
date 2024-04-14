using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField]
    private Image imgBg;
    [SerializeField]
    private TextMeshProUGUI txtHotkey;

    [SerializeField]
    private Color highlightColor;

    [SerializeField]
    private Color originalColor;

    private UIInventoryItem item;
    public bool HasItem { get { return item != null; } }

    public UIInventoryItem Item { get { return item; } }

    public void SetItem(UIInventoryItem item)
    {
        this.item = item;
    }

    public void Init(int number)
    {
        txtHotkey.text = $"{number}";
        name = $"Slot [{number}]";
    }

    public void Highlight()
    {
        imgBg.color = highlightColor;
    }
    public void Unhighlight()
    {
        imgBg.color = originalColor;
    }

    public InventoryItemConfig TakeItem()
    {
        if (item == null) { return null; }
        InventoryItemConfig itemConfig = item.Config;
        int itemsLeft = item.Take();
        if (itemsLeft < 1)
        {
            Destroy(item.gameObject);
            item = null;
        }
        return itemConfig;
    }

    public InventoryItemType? CheckItem()
    {
        if (item == null) { return null; }
        InventoryItemConfig itemConfig = item.Config;

        return itemConfig.Type;
    }

    public void DropItem()
    {
        item.Drop();
        item = null;
    }
}
