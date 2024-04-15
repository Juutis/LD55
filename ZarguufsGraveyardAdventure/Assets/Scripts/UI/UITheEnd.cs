using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITheEnd : MonoBehaviour
{
    public static UITheEnd main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject container;
    [SerializeField]

    private TextMeshProUGUI txtAchievements;

    [SerializeField]
    private UIGameRecord uiGameRecordPrefab;

    [SerializeField]
    private Transform gameRecordContainer;

    public void Open()
    {
        Time.timeScale = 0f;
        container.SetActive(true);
        List<PerkConfig> appliedPerks = PlayerPerkManager.main.AppliedPerks;
        txtAchievements.text = $"You collected {appliedPerks.Count} perks!";
        animator.Play("theEndShow");
    }

    public void OpenFinished()
    {

        //txtAchievements.text = $"You collected {appliedPerks.Count} perks!";
        foreach (var gameRecord in GameManager.main.GameRecords)
        {
            if (gameRecord.Value == 0)
            {
                continue;
            }
            UIGameRecord uiGameRecord = Instantiate(uiGameRecordPrefab, gameRecordContainer);
            uiGameRecord.Init(gameRecord);
        }
        /*
        isOpen = true;
        GameManager.main.StartGame();
        gameObject.SetActive(false);
        */
    }
}
