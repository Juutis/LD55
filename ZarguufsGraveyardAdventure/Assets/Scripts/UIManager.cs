using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private UIHealth uiHealth;

    int maxHealth = 20;
    void Start()
    {
        uiHealth.Init(maxHealth);
    }


    public void AddHealth(int health) {
        uiHealth.AddHealth(health);
    }
}
