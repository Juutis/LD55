using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
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

}