using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemPickup : MonoBehaviour
{
    private float itemCheckInterval = 0.15f;

    private float itemCheckTimer = 0f;

    private bool checkForItems = true;

    [SerializeField]
    private float maxDistance = 2f;

    [SerializeField]
    private KeyCode pickupKey = KeyCode.E;

    private bool pickUpEnabled = false;

    private List<PickupableItem> nearbyItems = new();

    // Update is called once per frame
    void Update()
    {
        if (!checkForItems)
        {
            return;
        }
        itemCheckTimer += Time.deltaTime;
        if (itemCheckTimer > itemCheckInterval)
        {
            itemCheckTimer = 0f;
            CheckForPickupableItems();
        }
        if (pickUpEnabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int index = nearbyItems.Count - 1; index >= 0; index -= 1)
                {
                    PickupableItem item = nearbyItems[index];
                    if (item == null)
                    {
                        continue;
                    }
                    bool itemWasPickedup = Inventory.main.AddItem(item.Config);
                    if (itemWasPickedup)
                    {
                        PickupableItemManager.main.KillItem(item);
                        nearbyItems.Remove(item);
                    }
                }
            }
        }
    }

    private void CheckForPickupableItems()
    {
        if (nearbyItems.Count > 0)
        {
            foreach (PickupableItem item in nearbyItems)
            {
                item.Unhighlight();
            }
        }
        nearbyItems = PickupableItemManager.main.CloseItems(transform.position, maxDistance);
        if (nearbyItems.Count > 0)
        {
            UIManager.main.ShowTooltip($"Pick up ({pickupKey})");
            pickUpEnabled = true;
            foreach (PickupableItem item in nearbyItems)
            {
                item.Highlight();
            }
        }
        else
        {
            UIManager.main.HideTooltip();
            pickUpEnabled = false;
        }
    }
}
