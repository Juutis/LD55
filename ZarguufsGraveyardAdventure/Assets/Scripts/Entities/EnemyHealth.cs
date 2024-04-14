using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    [SerializeField]
    private List<ItemDropChance> itemDrops;

    [SerializeField]
    private List<InventoryItemConfig> uniqueDrops;

    [SerializeField]
    private float itemDropChance;

    [SerializeField]
    private PerkGroup perkGroupPrefab;

    public void GetHit(int damage)
    {
        UIManager.main.ShowMessage(transform.position, $"-{damage}", Color.white);
        health -= damage;
        if (health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (Random.Range(0, 1f) <= itemDropChance && itemDrops.Count > 0)
        {
            List<InventoryItemConfig> itemChoices = new();
            foreach (ItemDropChance item in itemDrops)
            {
                // mult should be at least 1 to dodge possible bugs
                int mult = Mathf.Max(item.DropChanceMultiplier, 0);

                for (int i = 0; i < mult; i++)
                {
                    itemChoices.Add(item.Item);
                }
            }

            InventoryItemConfig drop = itemChoices[Random.Range(0, itemChoices.Count)];

            Vector2 circlePos = Random.insideUnitCircle.normalized;
            Vector3 pos = new(circlePos.x, circlePos.y, 0);

            PickupableItemManager.main.CreateItem(drop, transform.position + pos);
        }

        foreach (InventoryItemConfig item in uniqueDrops)
        {
            if (!Inventory.main.HasFoundUniqueItem(item.Type))
            {
                Vector2 circlePos = Random.insideUnitCircle.normalized;
                Vector3 pos = new(circlePos.x, circlePos.y, 0);
                PickupableItemManager.main.CreateItem(item, transform.position + pos);
            }
        }

        if (perkGroupPrefab != null)
        {
            PerkGroup group = Instantiate(perkGroupPrefab, transform.parent);
            group.transform.position = transform.position;
            group.Init();
        }

        Destroy(gameObject);
    }
}

[System.Serializable]
public class ItemDropChance
{
    public InventoryItemConfig Item;
    // if any item drops, it's DropChanceMultiplier times more likely to be this item
    public int DropChanceMultiplier;
}
