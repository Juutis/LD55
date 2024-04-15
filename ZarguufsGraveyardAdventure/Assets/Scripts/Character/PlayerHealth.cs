using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 10;
    private int maxHealth;

    private float invulnerabilityDuration;
    [SerializeField]
    private float invulnerabilityDurationFromDamage = 0.2f;
    [SerializeField]
    private float invulnerabilityDurationFromDash = 0.2f;
    private float invulnerabilityTimer = 0f;
    private bool isInvulnerable = false;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        maxHealth = health;
        UIManager.main.SetHealth(health);
    }

    public void InvulnerableFromDash()
    {
        isInvulnerable = true;
        invulnerabilityDuration = invulnerabilityDurationFromDash;
        invulnerabilityTimer = 0f;

    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > invulnerabilityDuration)
            {
                isInvulnerable = false;
                invulnerabilityTimer = 0f;

            }
        }
    }

    public void HealToFull()
    {
        health = maxHealth;
        UIManager.main.SetHealth(health);
    }

    public void IncreaseMaxHP(float value)
    {
        float currentPercentage = (float)health / maxHealth;
        maxHealth += Mathf.CeilToInt(value);
        int newHealth = Mathf.CeilToInt(maxHealth * currentPercentage);
        int change = newHealth - health;
        Debug.Log($"Old [{maxHealth - value}] New [{maxHealth}]. % [{currentPercentage}] so {health} should become {newHealth} and change is {change}");
        //Debug.Log($"increase max hp by {value} and hp by {change}");
        UIManager.main.SetMaxHealth(maxHealth);
        AddHealth(change);
    }

    public bool AddHealth(int healthAddition)
    {
        if (healthAddition < 0 && isInvulnerable)
        {
            return false;
        }
        if (healthAddition < 0)
        {
            isInvulnerable = true;
            invulnerabilityTimer = 0f;
            invulnerabilityDuration = invulnerabilityDurationFromDamage;
            SoundManager.main.PlaySound(GameSoundType.PlayerHurt);
        }
        if (healthAddition > 0 && health == maxHealth)
        {
            return false;
        }

        health += healthAddition;
        Debug.Log($"My health is now {health}");
        UIManager.main.AddHealth(healthAddition);
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        return true;
    }

    public void Die()
    {
        Time.timeScale = 0f;
        animator.Play("playerDie");
    }

    public void DieFinished()
    {
        GameManager.main.PlayerDie();
        animator.Play("playerIdle");
    }

}
