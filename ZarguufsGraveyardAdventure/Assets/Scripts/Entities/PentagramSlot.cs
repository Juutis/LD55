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

    private bool isLocked = false;

    [SerializeField]
    private Animator animator;


    void Start()
    {
        target = PlayerMovement.main.transform;
    }

    public void LockItem()
    {
        isLocked = true;
        item.Lock();
    }

    public void ConsumeItem()
    {
        animator.Play("pentagramSlotConsume");
    }

    public void ConsumeFinished()
    {
        PickupableItemManager.main.KillItem(item);
        Debug.Log("ConsumeFinished");
    }

    public void Unlock()
    {
        isLocked = false;
        playerIsAtSlot = false;
        checkTimer = 0f;
    }
    public void Clear()
    {
        item = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            if (playerIsAtSlot)
            {
                UIManager.main.HideWorldTooltip();
                playerIsAtSlot = false;
            }
            return;
        }
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
                UIManager.main.ShowWorldTooltip("Press 1-4 to place item here.", transform.position);
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
