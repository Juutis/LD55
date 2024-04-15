using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameRecord : MonoBehaviour
{

    [SerializeField]
    private Image imgIcon;
    [SerializeField]
    private TextMeshProUGUI txtTitle;
    [SerializeField]
    private TextMeshProUGUI txtDescription;
    [SerializeField]
    private TextMeshProUGUI txtCount;

    public void Init(GameRecord gameRecord)
    {
        imgIcon.sprite = gameRecord.Sprite;
        txtTitle.text = gameRecord.Name;
        txtDescription.text = gameRecord.Description;
        txtCount.text = $"{gameRecord.Value}";
    }

}
