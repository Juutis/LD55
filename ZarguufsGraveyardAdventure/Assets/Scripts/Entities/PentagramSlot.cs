using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramSlot : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float minDistance = 0.5f;
    private float checkInterval = 0.2f;
    private float checkTimer = 0f;

    private PickupableItem item = null;
    public PickupableItem Item { get { return item; } }

    private bool playerIsAtSlot = false;


    void Start()
    {
        target = PlayerMovement.main.transform;
    }


    public void ConsumeItem()
    {
        PickupableItemManager.main.KillItem(item);
    }

    public void Clear()
    {
        item = null;
    }

    // Update is called once per frame
    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > checkInterval)
        {
            checkTimer = 0f;
            if (Vector2.Distance(target.position, transform.position) < minDistance)
            {
                if (item != null)
                {
                    return;
                }
                UIManager.main.ShowWorldTooltip("Press 1-8 to place item here.", transform.position);
                playerIsAtSlot = true;
            }
            else if (playerIsAtSlot)
            {
                UIManager.main.HideWorldTooltip();
                playerIsAtSlot = false;
            }
        }
        if (item == null && playerIsAtSlot)
        {
            foreach (KeyCode keyCode in Inventory.main.InventoryKeys.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    InventoryItemConfig itemConfig = Inventory.main.TakeItem(Inventory.main.InventoryKeys[keyCode] - 1);
                    if (itemConfig != null)
                    {
                        PickupableItem pickupItem = PickupableItemManager.main.CreateItem(itemConfig, transform.position);
                        pickupItem.SetSlot(this);
                        item = pickupItem;
                        Pentagram pentagram = transform.parent.GetComponent<Pentagram>();
                        pentagram.CheckRecipes();
                        break;
                    }
                }
            }
        }
    }
}
