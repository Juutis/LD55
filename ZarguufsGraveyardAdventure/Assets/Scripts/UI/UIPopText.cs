using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPopText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtMessage;

    [SerializeField]
    private Animator animator;
    public void Show(Vector2 position, string message, Color color)
    {
        transform.position = position;
        txtMessage.text = message;
        txtMessage.color = color;
        animator.Play("uiPopTextShow");
    }
    public void Show(Vector2 position, string message)
    {
        Show(position, message, Color.white);
    }

    public void AnimationFinished()
    {
        Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
