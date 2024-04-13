using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private int health = 5;
    public void GetHit() {
        health -= 1;
        if (health < 0) {
            Die();
        }
    }
    
    public void Die() {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
