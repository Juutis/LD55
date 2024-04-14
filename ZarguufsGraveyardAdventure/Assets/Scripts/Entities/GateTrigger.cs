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
        if (playerIsAtGate)
        {
            foreach (KeyCode keyCode in Inventory.main.InventoryKeys.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    InventoryItemConfig itemConfig = Inventory.main.CheckItem(Inventory.main.InventoryKeys[keyCode] - 1);
                    // Debug.Log($"Using inventory item {itemConfig.Type.ToString()} {itemConfig != null} {itemConfig.IsKey}");

                    if (itemConfig != null && itemConfig.IsKey)
                    {
                        Inventory.main.TakeItem(Inventory.main.InventoryKeys[keyCode] - 1);
                        OpenGate();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!needsKey)
            {
                OpenGate();
            }
            else
            {
                UIManager.main.ShowWorldTooltip("Press 1-4 to use a key.", transform.position);
            }

            playerIsAtGate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerIsAtGate = false;
            UIManager.main.HideWorldTooltip();
        }
    }

    private void OpenGate()
    {
        openGate.SetActive(true);
        closedGate.SetActive(false);
        UIManager.main.HideWorldTooltip();
        doorTrigger.enabled = false;
    }
}
