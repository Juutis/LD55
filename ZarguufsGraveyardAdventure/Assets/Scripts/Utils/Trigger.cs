using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private string tag;

    private bool insideTrigger = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == tag)
        {
            insideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.tag == tag)
        {
            insideTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == tag)
        {
            insideTrigger = true;
        }
    }

    public bool InsideTrigger()
    {
        return insideTrigger;
    }
}
