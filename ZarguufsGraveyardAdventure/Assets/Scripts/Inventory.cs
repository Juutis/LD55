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

    /*
        private List<Image> inventoryItemImages;
        private List<InventoryItemConfig> collectedItems = new();
        */

    [SerializeField]
    private int size = 8;

    [SerializeField]
    private UIInventorySlot slotPrefab;
    [SerializeField]
    private UIInventoryItem itemPrefab;

    private List<UIInventorySlot> slots = new();

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

    public bool AddItem(InventoryItemConfig itemConfig)
    {
        if (itemConfig.IsStackable)
        {
            UIInventorySlot stackSlot = slots.FirstOrDefault(slot => slot.Item.Type == itemConfig.Type);
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

    /*void Start()
    {
        inventoryItemImages = new();
        foreach (Transform t in inventoryPanel.transform)
        {
            inventoryItemImages.Add(t.GetComponent<Image>()); //.Add(t.GetComponentInChildren<Image>());
        }
    }*/

    void Update()
    {
        /*
        inventoryItemImages.Skip(collectedItems.Count).ToList().ForEach(x => x.gameObject.SetActive(false));
        for (int i = 0; i < collectedItems.Count; i++)
        {
            if (inventoryItemImages.Count == i)
            {
                break;
            }

            InventoryItemConfig item = collectedItems.OrderBy(x => (int)x.Type).ToList()[i];
            inventoryItemImages[i].sprite = item.Sprite;
            inventoryItemImages[i].gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            collectedItems.Add(allItems[Random.Range(0, allItems.Count)]);
        }*/
    }
}

public enum InventoryItemType
{
    Bone = 10,
    Ectoplasm = 20,
    GateKey1 = 30,
    GateKey2 = 40,
    GateKey3 = 50
}