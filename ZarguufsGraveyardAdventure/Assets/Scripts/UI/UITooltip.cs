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
    private bool followPlayer = true;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private TextMeshProUGUI txtMessage;

    private void Start()
    {
        if (followPlayer)
        {
            followTarget.SetTarget(PlayerMovement.main.transform);
        }
    }

    public void Show(string message)
    {
        container.SetActive(true);
        txtMessage.text = message;
    }
    public void Show(string message, Vector2 position)
    {
        container.SetActive(true);
        txtMessage.text = message;
        container.transform.position = position;
    }
    public void Hide()
    {
        container.SetActive(false);
    }
}
