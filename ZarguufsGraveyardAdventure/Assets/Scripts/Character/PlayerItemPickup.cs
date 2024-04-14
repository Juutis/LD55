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

    private bool toolTipShown = false;

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
                int healthPickedUp = 0;

                for (int index = nearbyItems.Count - 1; index >= 0; index -= 1)
                {
                    PickupableItem item = nearbyItems[index];
                    if (item == null)
                    {
                        continue;
                    }

                    bool itemWasPickedup = false;
                    if (item.IsHealth())
                    {
                        if (PlayerMovement.main.PlayerHealth.AddHealth(1))
                        {
                            itemWasPickedup = true;
                            healthPickedUp++;
                        }
                    }
                    else
                    {
                        itemWasPickedup = Inventory.main.AddItem(item.Config);
                    }

                    if (itemWasPickedup)
                    {
                        PickupableItemManager.main.KillItem(item);
                        nearbyItems.Remove(item);
                    }
                }

                if (healthPickedUp > 0)
                {
                    UIManager.main.ShowMessage(transform.position, $"+{healthPickedUp}", Color.green);
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
            UIManager.main.ShowPlayerTooltip($"Pick up ({pickupKey})");
            toolTipShown = true;
            pickUpEnabled = true;
            foreach (PickupableItem item in nearbyItems)
            {
                item.Highlight();
            }
        }
        else
        {
            if (toolTipShown)
            {
                UIManager.main.HideTooltip();
            }
            toolTipShown = false;
            pickUpEnabled = false;
        }
    }
}
