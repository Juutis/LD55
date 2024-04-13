using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject closedGate;

    [SerializeField]
    private GameObject openGate;

    [SerializeField]
    private bool needsKey;

    // Start is called before the first frame update
    void Start()
    {
        openGate.SetActive(false);
        closedGate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            openGate.SetActive(true);
            closedGate.SetActive(false);
        }
    }
}
