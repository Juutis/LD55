using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickupableItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItemConfig config;

    public InventoryItemConfig Config { get { return config; } }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer highlightSpriteRenderer;
    [SerializeField]
    private SpriteRenderer animSpriteRenderer;
    [SerializeField]
    private bool initMyself;
    private PentagramSlot slot;

    private bool isLocked = false;
    public bool IsLocked { get { return isLocked; } }

    private UnityAction pickupAction;

    private void Start()
    {
        if (initMyself)
        {
            Init(config);
            PickupableItemManager.main.ActivateItem(this);
        }
    }

    public void Init(InventoryItemConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.Sprite;
        highlightSpriteRenderer.sprite = config.Sprite;
        animSpriteRenderer.sprite = config.Sprite;
    }

    public void RegisterPickupEffect(UnityAction action)
    {
        pickupAction = action;
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void SetSlot(PentagramSlot slot)
    {
        this.slot = slot;
    }
    public void Kill()
    {
        if (slot != null)
        {
            slot.Clear();
        }
        else
        {
            if (pickupAction != null)
            {
                pickupAction.Invoke();
            }
            if (config.Type == InventoryItemType.Nose)
            {
                GameManager.main.TheEnd();
            }
        }
        Destroy(gameObject);
    }

    public void Highlight()
    {
        if (highlightSpriteRenderer != null)
        {
            highlightSpriteRenderer.enabled = true;
        }
    }
    public void Unhighlight()
    {
        if (highlightSpriteRenderer != null)
        {

            highlightSpriteRenderer.enabled = false;
        }
    }

    public bool IsHealth()
    {
        return config.IsHealth;
    }
}
