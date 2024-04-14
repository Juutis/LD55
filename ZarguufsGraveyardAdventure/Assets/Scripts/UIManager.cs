using System.Collections;
using System.Collections.Generic;
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

    int maxHealth = 20;
    void Start()
    {
        uiHealth.Init(maxHealth);
    }


    public void WeaponCooldown(float cooldownDuration)
    {
        weaponActionSlot.Cooldown(cooldownDuration);
    }

    public void DashCooldown(float cooldownDuration)
    {
        dashActionSlot.Cooldown(cooldownDuration);
    }

    public void SetHealth(int health)
    {
        uiHealth.Init(health);
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
