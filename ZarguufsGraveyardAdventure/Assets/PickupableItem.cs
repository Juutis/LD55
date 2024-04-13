using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField]
    private InventoryItemConfig config;

    public InventoryItemConfig Config { get { return config; } }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void Init(InventoryItemConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.Sprite;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
