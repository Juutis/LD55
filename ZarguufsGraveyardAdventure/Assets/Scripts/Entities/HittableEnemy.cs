using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HittableEnemy : MonoBehaviour
{
    EnemyHealth enemyHealth;

    void Start() {
        enemyHealth = GetComponent<EnemyHealth>();
    }
    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.tag == "PlayerWeapon") {
            Debug.Log($"[{name}] was hit by [{collider2D.name}]");
            enemyHealth.GetHit();
        }
    }

}
