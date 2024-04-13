using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject inventoryPanel;
    [SerializeField]
    private List<InventoryItemConfig> allItems;

    private List<Image> inventoryItemImages;
    private List<InventoryItemConfig> collectedItems = new();

    void Start()
    {
        inventoryItemImages = new();
        foreach (Transform t in inventoryPanel.transform)
        {
            inventoryItemImages.Add(t.GetComponent<Image>()); //.Add(t.GetComponentInChildren<Image>());
        }
    }

    void Update()
    {
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
        }
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