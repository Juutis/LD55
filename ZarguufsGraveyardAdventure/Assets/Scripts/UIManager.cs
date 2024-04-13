using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private UIHealth uiHealth;
    [SerializeField]
    private UITooltip uiTooltip;


    int maxHealth = 20;
    void Start()
    {
        uiHealth.Init(maxHealth);
    }


    public void AddHealth(int health)
    {
        uiHealth.AddHealth(health);
    }

    public void ShowTooltip(string message)
    {
        uiTooltip.Show(message);
    }

    public void HideTooltip()
    {
        uiTooltip.Hide();
    }
}
