using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HittableEnemy : MonoBehaviour
{
    EnemyHealth enemyHealth;
    RunTowardsTargetEnemy runTowards;

    private float invulnerabilityDuration = 0.1f;
    private float invulnerabilityTimer;
    private bool isInvulnerable = false;


    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        runTowards = GetComponent<RunTowardsTargetEnemy>();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > invulnerabilityDuration)
            {
                invulnerabilityTimer = 0;
                isInvulnerable = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (isInvulnerable)
        {
            return;
        }
        // Debug.Log($"hit by {collider2D}");
        if (collider2D.tag == "PlayerWeapon")
        {
            isInvulnerable = true;
            // Debug.Log($"[{name}] was hit by [{collider2D.name}]");
            enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemyHealth.enabled) {
                enemyHealth.GetHit(PlayerAiming.main.Damage);
            }
            /*Debug.Log($"Forcing u back at {-body.velocity * knockBackStrength}!");
            body.AddForce(-body.velocity * knockBackStrength, ForceMode2D.Impulse);*/
            if (runTowards != null)
            {
                runTowards.KnockBack(PlayerAiming.main.KnockbackStrength, PlayerAiming.main.KnockbackDuration);
            }
        }
    }

}
