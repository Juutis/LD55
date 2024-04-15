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
    private EnemyType enemyType;

    private InventoryItemConfig forcedDrop;


    public void GetHit(int damage)
    {
        UIManager.main.ShowMessage(transform.position, $"-{damage}", Color.white);
        health -= damage;
        SoundManager.main.PlaySound(GameSoundType.SwingHit);
        if (health < 0)
        {
            Die();
        }
    }

    public void AddForcedDrop(InventoryItemConfig item)
    {
        forcedDrop = item;
    }


    public void Die()
    {
        if (forcedDrop != null)
        {
            PickupableItemManager.main.CreateItem(forcedDrop, transform.position);
        }
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

            PickupableItemManager.main.CreateItem(drop, transform.position);
        }

        foreach (InventoryItemConfig item in uniqueDrops)
        {
            if (!Inventory.main.HasFoundUniqueItem(item.Type))
            {
                Vector2 circlePos = Random.insideUnitCircle.normalized * 0.5f;
                Vector3 pos = new(circlePos.x, circlePos.y, 0);

                if (uniqueDrops.Count == 1)
                {
                    pos = Vector3.zero;
                }

                PickupableItemManager.main.CreateItem(item, transform.position + pos);
            }
        }

        BossEnemy bossEnemy = GetComponent<BossEnemy>();
        if (bossEnemy != null)
        {
            bossEnemy.Die();
        }
        else
        {
            if (enemyType == EnemyType.Skeleton)
            {
                SoundManager.main.PlaySound(GameSoundType.SkeletonDie);
                GameManager.main.AddRecord(GameRecordType.KillSkeleton, 1);
            }
            else if (enemyType == EnemyType.Zombie)
            {
                SoundManager.main.PlaySound(GameSoundType.ZombieDie);
                GameManager.main.AddRecord(GameRecordType.KillZombie, 1);
            }
            else if (enemyType == EnemyType.Ghost)
            {
                SoundManager.main.PlaySound(GameSoundType.GhostDie);
                GameManager.main.AddRecord(GameRecordType.KillGhost, 1);

            }
            Destroy(gameObject);
        }

    }
}


public enum EnemyType
{
    Skeleton,
    Zombie,
    Ghost,
    Boss
}

[System.Serializable]
public class ItemDropChance
{
    public InventoryItemConfig Item;
    // if any item drops, it's DropChanceMultiplier times more likely to be this item
    public int DropChanceMultiplier;
}
