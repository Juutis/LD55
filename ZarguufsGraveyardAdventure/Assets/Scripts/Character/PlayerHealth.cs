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
        Debug.Log("Cant die!!");
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
                Debug.Log("Not anymore!");
            }
        }
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
        }
        health += healthAddition;
        UIManager.main.AddHealth(healthAddition);
        if (health < 0)
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
        Debug.Log("You died!");
    }

}
