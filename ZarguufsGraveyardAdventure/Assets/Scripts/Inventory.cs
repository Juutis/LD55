using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private List<InventoryItemConfig> allItems;

    [SerializeField]
    private int size = 8;

    [SerializeField]
    private UIInventorySlot slotPrefab;
    [SerializeField]
    private UIInventoryItem itemPrefab;

    private List<InventoryItemType> foundUniqueItems = new();
    private List<UIInventorySlot> slots = new();

    public Dictionary<KeyCode, int> InventoryKeys = new(){
        {KeyCode.Alpha1, 1},
        {KeyCode.Alpha2, 2},
        {KeyCode.Alpha3, 3},
        {KeyCode.Alpha4, 4},
        {KeyCode.Alpha5, 5},
        {KeyCode.Alpha6, 6},
        {KeyCode.Alpha7, 7},
        {KeyCode.Alpha8, 8},
    };

    void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int index = 0; index < size; index += 1)
        {
            UIInventorySlot slot = Instantiate(slotPrefab, inventoryPanel.transform);
            slot.Init(index + 1);
            slots.Add(slot);
        }
    }

    public InventoryItemConfig TakeItem(int index)
    {
        if (index < 0 || index >= slots.Count)
        {
            return null;
        }
        UIInventorySlot slot = slots[index];

        return slot.TakeItem();
    }

    public InventoryItemConfig CheckItem(int index)
    {
        if (index < 0 || index >= slots.Count)
        {
            return null;
        }
        UIInventorySlot slot = slots[index];

        return slot.CheckItem();
    }

    public bool AddItem(InventoryItemConfig itemConfig)
    {
        if (itemConfig.IsStackable)
        {
            UIInventorySlot stackSlot = slots.FirstOrDefault(slot => slot.Item != null && slot.Item.Type == itemConfig.Type);
            if (stackSlot != null)
            {
                stackSlot.Item.AddStackableItemCount(itemConfig.Count);
                return true;
            }
        }
        UIInventorySlot slot = slots.FirstOrDefault(slot => !slot.HasItem);
        if (slot == null)
        {
            return false;
        }
        UIInventoryItem item = Instantiate(itemPrefab, slot.transform);
        item.Init(itemConfig);
        slot.SetItem(item);
        return true;
    }

    public bool AddUniqueItem(InventoryItemConfig itemConfig)
    {
        if (!foundUniqueItems.Contains(itemConfig.Type))
        {
            foundUniqueItems.Add(itemConfig.Type);
        }

        return AddItem(itemConfig);
    }

    public bool HasFoundUniqueItem(InventoryItemType type)
    {
        Debug.Log(string.Join(", ", foundUniqueItems));
        return foundUniqueItems.Contains(type);
    }

}

public enum InventoryItemType
{
    Bone = 10,
    Ectoplasm = 20,
    GateKey1 = 30,
    GateKey2 = 40,
    GateKey3 = 50,
    Heart = 60,
    Eye = 70,
    Hand = 80,
    StartKey = 90,
}