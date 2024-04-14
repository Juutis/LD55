using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    private Collider2D playerCollider;

    [SerializeField]
    private SpriteRenderer spriteRenderer;


    [SerializeField]
    private float speed;
    private float horizontal;
    private float vertical;

    [SerializeField]
    private float dashSpeed = 5;
    [SerializeField]
    private KeyCode dashKey = KeyCode.Space;

    [SerializeField]
    private PlayerHealth playerHealth;

    private float currentSpeed;
    private float dashCooldownDuration = 2f;
    private float dashCooldownTimer = 0f;
    private bool dashIsOnCooldown = false;


    private bool isDashing = false;
    public bool IsDashing { get { return isDashing; } }

    private float dashDuration = 0.2f;
    private float dashTimer = 0f;

    private Animator anim;

    private Vector2 dashDirection = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (dashIsOnCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            if (dashCooldownTimer > dashCooldownDuration)
            {
                dashCooldownTimer = 0f;
                dashIsOnCooldown = false;
            }
        }
        if (!dashIsOnCooldown && !isDashing && Input.GetKeyDown(dashKey))
        {
            isDashing = true;
            anim.Play("playerDash");
            gameObject.layer = LayerMask.NameToLayer("DashingPlayer");
            currentSpeed = dashSpeed;
            dashIsOnCooldown = true;
            dashCooldownTimer = 0f;
            UIManager.main.DashCooldown(dashCooldownDuration);
            playerHealth.InvulnerableFromDash();
        }
        else if (isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer > dashDuration)
            {
                anim.Play("playerIdle");
                gameObject.layer = LayerMask.NameToLayer("Player");
                dashTimer = 0f;
                isDashing = false;
                currentSpeed = speed;
                playerHealth.InvulnerableFromDash();
            }
        }
        else
        {
            HandleWalk();
        }
    }


    void HandleWalk()
    {
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
            dashDirection = body.velocity.normalized;
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    void FixedUpdate()
    {
        var dir = new Vector2(horizontal, vertical).normalized;
        if (isDashing)
        {
            dir = dashDirection;
        }
        body.velocity = dir * currentSpeed;
    }

}