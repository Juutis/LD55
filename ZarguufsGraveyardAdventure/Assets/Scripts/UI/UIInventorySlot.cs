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

    public void UseItem()
    {
        item.Use();
    }
    public void DropItem()
    {
        item.Drop();
        item = null;
    }
}