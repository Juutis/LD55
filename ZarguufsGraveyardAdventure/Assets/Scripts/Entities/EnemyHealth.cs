using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 5;

    [SerializeField]
    private List<ItemDropChance> itemDrops;

    [SerializeField]
    private float itemDropChance;

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
        Debug.Log("Enemy died!");

        if (Random.Range(0,1f) <= itemDropChance)
        {
            List<InventoryItemConfig> itemChoices = new();
            foreach (ItemDropChance item in itemDrops)
            {
                for (int i = 0; i < item.DropChanceMultiplier; i++)
                {
                    itemChoices.Add(item.Item);
                }
            }

            InventoryItemConfig drop = itemChoices[Random.Range(0, itemChoices.Count)];
            PickupableItemManager.main.CreateItem(drop, transform.position);
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
