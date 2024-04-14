using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class GateTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject closedGate;

    [SerializeField]
    private GameObject openGate;

    [SerializeField]
    private BoxCollider2D doorTrigger;

    [SerializeField]
    private bool needsKey;

    private bool playerIsAtGate = false;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        openGate.SetActive(false);
        closedGate.SetActive(true);
        doorTrigger.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            return;
        }
        if (playerIsAtGate)
        {
            foreach (KeyCode keyCode in Inventory.main.InventoryKeys.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    InventoryItemConfig itemConfig = Inventory.main.CheckItem(Inventory.main.InventoryKeys[keyCode] - 1);
                    //Debug.Log($"Using inventory item {itemConfig.Type.ToString()} {itemConfig != null} {itemConfig.IsKey}");

                    if (itemConfig != null && itemConfig.IsKey)
                    {
                        Inventory.main.TakeItem(Inventory.main.InventoryKeys[keyCode] - 1);
                        OpenGate();
                        return;
                    }
                }
            }

            UIManager.main.ShowWorldTooltip("Press 1-8 to use a key.", transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            if (!needsKey)
            {
                OpenGate();
            }

            playerIsAtGate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen)
        {
            return;
        }
        if (collision.tag == "Player")
        {
            playerIsAtGate = false;
            UIManager.main.HideWorldTooltip();
        }
    }

    private void OpenGate()
    {
        Debug.Log("Gate Opened");
        openGate.SetActive(true);
        closedGate.SetActive(false);
        isOpen = true;
        UIManager.main.HideWorldTooltip();
        doorTrigger.enabled = false;
    }
}
