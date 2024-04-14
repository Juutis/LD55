using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pentagram : MonoBehaviour
{

    [SerializeField]
    private List<PentagramRecipe> recipes = new();

    [SerializeField]
    private Animator animator;

    private List<PentagramSlot> slots = new();

    private bool isSummoning = false;
    private bool recipeIsChosen = false;
    private PentagramRecipe chosenRecipe;

    private List<PentagramSlot> filledSlots = new();

    private float itemConsumeInterval = 0.2f;
    private float itemConsumeTimer = 0f;
    private float bossSummonDelay = 1f;

    [SerializeField]
    private Transform bossSpawnPosition;

    [SerializeField]
    private Transform perkSpawnPosition;
    public Transform PerkSpawnPosition { get { return perkSpawnPosition; } }


    void Start()
    {
        slots = GetComponentsInChildren<PentagramSlot>().ToList();
    }
    // Start is called before the first frame update
    public void CheckRecipes()
    {
        if (recipeIsChosen || isSummoning)
        {
            return;
        }
        List<InventoryItemConfig> items = slots.Where(slot => slot.Item != null).Select(slot => slot.Item.Config).ToList();
        if (items.Count == 5)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Match(items))
                {
                    recipeIsChosen = true;
                    foreach (var slot in slots)
                    {
                        slot.LockItem();
                    }
                    chosenRecipe = recipe;
                    animator.Play("pentagramSummonStart");
                    Invoke("PerformRecipe", bossSummonDelay);
                    break;
                }
            }

        }
    }

    public void Unlock()
    {
        animator.Play("pentagramSummonEnd");
    }

    public void PentagramSummonEndFinished()
    {
        foreach (var slot in slots)
        {
            slot.Unlock();
        }
    }

    public void PerformRecipe()
    {
        isSummoning = true;
        filledSlots = new(slots);
        ShuffleSlots(filledSlots);

        Debug.Log($"We shall summon {chosenRecipe.BossType}!!!");
        itemConsumeTimer = 0f;
    }

    private void ShuffleSlots(List<PentagramSlot> slotList)
    {
        System.Random r = new System.Random();
        slotList.Sort((x, y) => r.Next(-1, 1));
    }


    private void Update()
    {
        if (!isSummoning)
        {
            return;
        }
        itemConsumeTimer += Time.deltaTime;
        if (itemConsumeTimer > itemConsumeInterval)
        {
            itemConsumeTimer = 0f;
            PentagramSlot slot = filledSlots[0];
            filledSlots.RemoveAt(0);
            slot.ConsumeItem();
            if (filledSlots.Count == 0)
            {
                Invoke("SummonBoss", bossSummonDelay);
                isSummoning = false;
            }
        }
    }

    private void SummonBoss()
    {
        GameObject bossInstance = Instantiate(chosenRecipe.BossPrefab);
        bossInstance.transform.position = bossSpawnPosition.position;
        BossEnemy bossEnemy = bossInstance.GetComponent<BossEnemy>();
        bossEnemy.Init(this);
        GameManager.main.RegisterBoss(bossEnemy);
        chosenRecipe = null;

        recipeIsChosen = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Debug.Log("Player walked on the sacered pentagram!!!!!");
        }
    }
}

public enum BossType
{
    None,
    BoneBoss,
    DemonBoss,
    NoseBoss
}

[System.Serializable]
public class PentagramRecipe
{
    public List<InventoryItemType> RequiredItems;
    public BossType BossType;
    public GameObject BossPrefab;

    public bool Match(List<InventoryItemConfig> items)
    {
        List<InventoryItemConfig> clonedItems = new(items);
        foreach (var type in RequiredItems)
        {
            InventoryItemConfig requiredItem = clonedItems.FirstOrDefault(item => item.Type == type);
            if (requiredItem == null)
            {
                return false;
            }
            clonedItems.Remove(requiredItem);
        }
        return true;
    }
}
