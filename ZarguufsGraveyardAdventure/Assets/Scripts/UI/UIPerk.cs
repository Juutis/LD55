using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPerk : MonoBehaviour
{
    [SerializeField]
    private Image imgIcon;
    [SerializeField]
    private TextMeshProUGUI txtCount;

    PerkConfig config;
    public PerkConfig Config { get { return config; } }
    int count = 0;
    public void Init(PerkConfig perkConfig)
    {
        config = perkConfig;
        imgIcon.sprite = perkConfig.Sprite;
        AddCount();
    }
    public void AddCount()
    {
        count += 1;
        txtCount.text = $"{count}";
    }
}
