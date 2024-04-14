using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perk", menuName = "Configs/PerkConfig", order = 1)]
public class PerkConfig : ScriptableObject
{
    public PerkType Type;
    public Sprite Sprite;
    public string Title;
    [TextArea]
    public string Description;

    public int Value = 1;
}


public enum PerkType
{
    None,
    WeaponDamage,
    WeaponReach,
    DashLength,
    DashCooldown,
    Health,
    WalkSpeed
}