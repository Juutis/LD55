using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIActionSlot : MonoBehaviour
{
    private float cooldownDuration;
    private float cooldownTimer = 0f;

    private bool isOnCooldown = false;

    [SerializeField]
    private Image imgCooldownIndicator;

    public void Cooldown(float duration)
    {
        cooldownDuration = duration;
        isOnCooldown = true;
        cooldownTimer = 0f;
        imgCooldownIndicator.fillAmount = 1;
        imgCooldownIndicator.enabled = true;
    }

    void Update()
    {
        if (!isOnCooldown)
        {
            return;
        }
        cooldownTimer += Time.deltaTime;
        imgCooldownIndicator.fillAmount = 1 - (cooldownTimer / cooldownDuration);
        if (cooldownTimer >= cooldownDuration)
        {
            imgCooldownIndicator.fillAmount = 0;
            isOnCooldown = false;
            cooldownTimer = 0;
            imgCooldownIndicator.enabled = false;
        }
    }
}
