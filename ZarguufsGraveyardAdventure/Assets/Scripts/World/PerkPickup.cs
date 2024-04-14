using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PerkPickup : MonoBehaviour
{
    [SerializeField]
    private PerkConfig config;

    public PerkConfig Config { get { return config; } }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private SpriteRenderer highlightSpriteRenderer;

    public void Init(PerkConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.Sprite;
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
}
