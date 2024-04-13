using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement main;
    void Awake()
    {
        main = this;
    }
    private Rigidbody2D body;
    [SerializeField]
    private SpriteRenderer spriteRenderer;


    [SerializeField]
    private float speed;
    private float horizontal;
    private float vertical;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (body.velocity.magnitude > 0.01f)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    void FixedUpdate()
    {
        var dir = new Vector2(horizontal, vertical).normalized;
        body.velocity = dir * speed;
    }

    /*void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if (collider2D.tag == "PickupableItem")
        {
            PickupableItem pickupableItem = collider2D.GetComponent<PickupableItem>();
            if (pickupableItem != null)
            {
                Inventory.main.AddItem(pickupableItem.Config);
                pickupableItem.Kill();
            }
        }
    }*/

}