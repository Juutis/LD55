using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private float damageDealingInterval = 1.5f;
    private float damageDealingTimer = 0f;
    private bool damageCooldown = false;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        HandleDamageDealing(collision2D);
    }
    void OnCollisionStay2D(Collision2D collision2D)
    {
        HandleDamageDealing(collision2D);
    }
    void OnCollisionExit2D(Collision2D collision2D)
    {
        damageCooldown = false;
    }

    void HandleDamageDealing(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Player")
        {
            if (damageCooldown)
            {
                return;
            }

            //Debug.Log($"[Player] was hit by [{name}] for {damage} damage");
            PlayerHealth playerHealth = collision2D.gameObject.GetComponent<PlayerHealth>();
            bool damageSuccesful = playerHealth.AddHealth(-damage);
            if (damageSuccesful)
            {
                UIManager.main.ShowMessage(collision2D.GetContact(0).point, $"-{damage}", Color.red);
                damageCooldown = true;
                damageDealingTimer = 0f;
            }
        }
    }

    void Update()
    {
        if (damageCooldown)
        {
            damageDealingTimer += Time.deltaTime;
            if (damageDealingTimer > damageDealingInterval)
            {
                damageCooldown = false;
                damageDealingTimer = 0f;
            }
        }
    }
}
