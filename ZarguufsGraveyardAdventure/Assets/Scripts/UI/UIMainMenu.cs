using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool isOpen = true;
    private bool isOpening = false;

    void Update()
    {
        if (!isOpen || isOpening)
        {
            return;
        }
        if (Input.anyKeyDown)
        {
            isOpening = true;
            animator.Play("mainMenuClose");
        }
    }

    public void CloseFinished()
    {
        isOpen = true;
        GameManager.main.StartGame();
        gameObject.SetActive(false);
    }
}
