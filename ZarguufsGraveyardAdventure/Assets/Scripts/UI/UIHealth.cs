using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    private int health;
    private int maxHealth;

    [SerializeField]
    private Image imgHealth;
    [SerializeField]
    private TextMeshProUGUI txtHealth;

    public void Init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        SetHealth(maxHealth);
    }

    public void AddHealth(int healthChange)
    {
        SetHealth(health + healthChange);
    }
    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        SetHealth(health);
    }

    private void SetHealth(int health)
    {
        if (health < 0)
        {
            health = 0;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }
        this.health = health;
        imgHealth.fillAmount = (float)health / maxHealth;
        txtHealth.text = $"{health}/{maxHealth}";
    }
}
