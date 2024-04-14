using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pentagram : MonoBehaviour
{

    [SerializeField]
    private List<PentagramRecipe> recipes = new();

    private List<PentagramSlot> slots = new();

    void Start()
    {
        slots = GetComponentsInChildren<PentagramSlot>().ToList();
    }
    // Start is called before the first frame update
    public void CheckRecipes()
    {
        List<InventoryItemConfig> items = slots.Where(slot => slot.Item != null).Select(slot => slot.Item.Config).ToList();
        if (items.Count == 5)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.Match(items))
                {
                    PerformRecipe(recipe);
                    break;
                }
            }

        }
    }

    public void PerformRecipe(PentagramRecipe recipe)
    {
        foreach (var slot in slots)
        {
            slot.ConsumeItem();
        }
        Debug.Log($"We shall summon {recipe.BossType}!!!");

        GameObject bossInstance = Instantiate(recipe.BossPrefab);
        bossInstance.transform.position = transform.position;
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
