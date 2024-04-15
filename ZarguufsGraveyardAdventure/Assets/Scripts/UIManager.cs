using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private Transform worldSpaceUI;
    [SerializeField]
    private GameObject gameUI;
    [SerializeField]
    private UIPopText popTextPrefab;

    [SerializeField]
    private UIHealth uiHealth;
    [SerializeField]
    private UITooltip uiTooltip;
    [SerializeField]
    private UITooltip uiWorldTooltip;


    [SerializeField]
    private UIActionSlot weaponActionSlot;
    [SerializeField]
    private UIActionSlot dashActionSlot;

    [SerializeField]
    private UIPerk uiPerkPrefab;
    private List<UIPerk> uiPerks = new();
    [SerializeField]
    private GameObject uiPerkArea;
    [SerializeField]
    private Transform uiPerkContainer;

    public void TurnOnUI()
    {
        gameUI.SetActive(true);
    }

    public void WeaponCooldown(float cooldownDuration)
    {
        weaponActionSlot.Cooldown(cooldownDuration);
    }

    public void DashCooldown(float cooldownDuration)
    {
        dashActionSlot.Cooldown(cooldownDuration);
    }

    public void AddPerk(PerkConfig perkConfig)
    {
        if (uiPerks.Count == 0)
        {
            uiPerkArea.SetActive(true);
        }
        UIPerk existingPerk = uiPerks.FirstOrDefault(perk => perk.Config.Type == perkConfig.Type);
        if (existingPerk != null)
        {
            existingPerk.AddCount();
        }
        else
        {
            UIPerk uiPerk = Instantiate(uiPerkPrefab, uiPerkContainer);
            uiPerk.Init(perkConfig);
            uiPerks.Add(uiPerk);
        }
    }

    public void SetHealth(int health)
    {
        uiHealth.Init(health);
    }
    public void SetMaxHealth(int maxHealth)
    {
        uiHealth.SetMaxHealth(maxHealth);
    }
    public void AddHealth(int healthChange)
    {
        uiHealth.AddHealth(healthChange);
    }

    public void ShowPlayerTooltip(string message)
    {
        uiTooltip.Show(message);
    }

    public void ShowWorldTooltip(string message, Vector2 position)
    {
        uiWorldTooltip.Show(message, position);
    }

    public void ShowMessage(Vector2 position, string message, Color color)
    {
        UIPopText popText = Instantiate(popTextPrefab, worldSpaceUI);
        popText.Show(position, message, color);
    }
    public void ShowMessage(Vector2 position, string message)
    {
        ShowMessage(position, message, Color.white);
    }

    public void HideTooltip()
    {
        uiTooltip.Hide();
    }
    public void HideWorldTooltip()
    {
        uiWorldTooltip.Hide();
    }
}
