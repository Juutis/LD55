using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PerkGroup : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float minDistance = 1f;
    private float checkInterval = 0.1f;
    private float checkTimer = 0f;

    private PerkPickup nearbyPerk;

    [Header("3 by random, count increases chance.")]
    [SerializeField]
    private List<RandomPerk> perkBag = new();

    private List<PerkPickup> perks = new();

    [SerializeField]
    private KeyCode keyCode = KeyCode.E;

    private UnityAction pickupAction;

    void Start()
    {
        Init();
    }


    public void Init()
    {
        perks = GetComponentsInChildren<PerkPickup>().ToList();
        target = PlayerMovement.main.transform;
        RandomizePerks();
    }

    public void RegisterPickupEffect(UnityAction action)
    {
        pickupAction = action;
    }

    private void RandomizePerks()
    {
        List<PerkConfig> bag = new();
        foreach (var randomPerk in perkBag)
        {
            for (int i = 0; i < randomPerk.Count; i += 1)
            {
                bag.Add(randomPerk.Config);
            }
        }
        for (int index = 0; index < 3; index += 1)
        {
            PerkConfig config = bag[Random.Range(0, bag.Count)];
            for (int i = bag.Count - 1; i >= 0; i -= 1)
            {
                if (bag[i].Type == config.Type)
                {
                    bag.RemoveAt(i);
                }
            }
            Debug.Log(index);
            perks[index].Init(config);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RandomizePerks();
        }
        checkTimer += Time.deltaTime;
        if (checkTimer > checkInterval)
        {
            checkTimer = 0f;
            bool playerWasNear = false;
            foreach (var perk in perks)
            {
                perk.Unhighlight();
                if (Vector2.Distance(target.position, perk.transform.position) < minDistance)
                {
                    Vector2 newPos = perk.transform.position;
                    newPos.y = newPos.y + 0.8f;
                    UIManager.main.ShowPlayerTooltip($"Press {keyCode} to choose perk.");
                    UIManager.main.ShowWorldTooltip($"{perk.Config.Title}\n {perk.Config.Description}", newPos);
                    nearbyPerk = perk;
                    playerWasNear = true;
                    nearbyPerk.Highlight();
                }
            }
            if (!playerWasNear)
            {
                if (nearbyPerk)
                {
                    UIManager.main.HideTooltip();
                    UIManager.main.HideWorldTooltip();
                }
                nearbyPerk = null;
            }
        }
        if (nearbyPerk != null)
        {
            if (Input.GetKeyDown(keyCode))
            {
                PerkConfig config = nearbyPerk.Config;
                PlayerPerkManager.main.ApplyPerk(config);
                foreach (var perk in perks)
                {
                    perk.Kill();
                }
                UIManager.main.HideTooltip();
                UIManager.main.HideWorldTooltip();
                if (pickupAction != null)
                {
                    pickupAction.Invoke();
                }
                Destroy(gameObject);
            }
        }
    }
}

[System.Serializable]
public class RandomPerk
{
    public PerkConfig Config;
    public int Count = 1;
}