using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItemConfig config;

    public InventoryItemConfig Config { get { return config; } }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer highlightSpriteRenderer;
    private PentagramSlot slot;

    public void Init(InventoryItemConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.Sprite;
        highlightSpriteRenderer.sprite = config.Sprite;
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
