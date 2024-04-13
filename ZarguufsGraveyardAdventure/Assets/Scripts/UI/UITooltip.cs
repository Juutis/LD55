using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UITooltip : MonoBehaviour
{
    [SerializeField]
    private FollowTarget followTarget;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private TextMeshProUGUI txtMessage;

    private void Start()
    {
        followTarget.SetTarget(PlayerMovement.main.transform);
    }

    public void Show(string message)
    {
        container.SetActive(true);
        txtMessage.text = message;
    }
    public void Hide()
    {
        container.SetActive(false);
    }
}
