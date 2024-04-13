using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PickupableItemManager : MonoBehaviour
{

    public static PickupableItemManager main;
    void Awake()
    {
        main = this;
    }
    private List<PickupableItem> items = new();

    [SerializeField]
    private InventoryItemConfig testItem;

    [SerializeField]
    private PickupableItem itemPrefab;

    public void CreateItem(InventoryItemConfig itemConfig, Vector3 position)
    {
        PickupableItem item = Instantiate(itemPrefab, transform);
        item.Init(itemConfig);
        item.transform.position = position;
        AddItem(item);
    }

    public List<PickupableItem> CloseItems(Vector3 position, float maxDistance)
    {
        return items.Where(item => Vector3.Distance(position, item.transform.position) <= maxDistance).ToList();
    }

    private void AddItem(PickupableItem item)
    {
        items.Add(item);
    }

    public void KillItem(PickupableItem item)
    {
        item.Unhighlight();
        items.Remove(item);
        item.Kill();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateItem(testItem, PlayerMovement.main.transform.position);
        }
    }
}
