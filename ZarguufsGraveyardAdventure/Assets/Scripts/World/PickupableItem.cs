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

    public void Init(InventoryItemConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.Sprite;
        highlightSpriteRenderer.sprite = config.Sprite;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Highlight()
    {
        highlightSpriteRenderer.enabled = true;
    }
    public void Unhighlight()
    {
        highlightSpriteRenderer.enabled = false;
    }

    public bool IsHealth()
    {
        return config.IsHealth;
    }
}
