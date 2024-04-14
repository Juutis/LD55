using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkManager : MonoBehaviour
{
    public static PlayerPerkManager main;
    void Awake()
    {
        main = this;
    }
    private List<PerkConfig> appliedPerks = new();
    private PlayerMovement playerMovement;
    private PlayerAiming playerAiming;
    private PlayerHealth playerHealth;
    void Start()
    {
        playerMovement = PlayerMovement.main;
        playerAiming = PlayerAiming.main;
        playerHealth = playerMovement.PlayerHealth;
    }
    public void ApplyPerk(PerkConfig perk)
    {
        // here
        Debug.Log($"We are applying perk: {perk.Title}");
        switch (perk.Type)
        {
            case PerkType.Health:
                playerHealth.IncreaseMaxHP(perk.Value);
                break;
            case PerkType.DashCooldown:
                playerMovement.ReduceDashCooldown(perk.Value);
                break;
            case PerkType.DashLength:
                playerMovement.IncreaseDashLength(perk.Value);
                break;
            case PerkType.WalkSpeed:
                playerMovement.IncreaseWalkSpeed(perk.Value);
                break;
            case PerkType.WeaponReach:
                playerAiming.IncreaseWeaponReach(perk.Value);
                break;
            case PerkType.WeaponKnockback:
                playerAiming.IncreaseWeaponKnockback(perk.Value);
                break;
            case PerkType.WeaponDamage:
                playerAiming.IncreaseWeaponDamage(perk.Value);
                break;
        }
        appliedPerks.Add(perk);
    }
}
