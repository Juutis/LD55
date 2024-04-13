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
    private UIHealth uiHealth;
    [SerializeField]
    private UITooltip uiTooltip;


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

    public void AddHealth(int health)
    {
        uiHealth.AddHealth(health);
    }

    public void ShowTooltip(string message)
    {
        uiTooltip.Show(message);
    }

    public void HideTooltip()
    {
        uiTooltip.Hide();
    }
}
