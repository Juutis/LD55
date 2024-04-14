using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    public static PlayerAiming main;
    void Awake()
    {
        main = this;
    }
    [SerializeField]
    private GameObject aimingReticule;

    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private SpriteRenderer weaponRenderer;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int damage = 2;
    public int Damage { get { return damage; } }

    [SerializeField]
    private float cooldownDuration = 0.5f;
    private float cooldownTimer = 0f;

    private bool isOnCooldown = false;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimingReticule.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);

        Vector3 targetDir = GetDirection();
        float angleDiff = Vector2.SignedAngle(hand.transform.right, targetDir);
        hand.transform.Rotate(Vector3.forward, angleDiff);

        if (worldPosition.x < transform.position.x)
        {
            weaponRenderer.flipY = true;
        }
        else
        {
            weaponRenderer.flipY = false;
        }

        if (isOnCooldown)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownDuration)
            {
                cooldownTimer = 0f;
                isOnCooldown = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("MouseButton 0");
            if (isOnCooldown)
            {
                return;
            }
            SwingWeapon();
        }
    }

    private void SwingWeapon()
    {
        isOnCooldown = true;
        cooldownTimer = 0f;
        Debug.Log("Swing");
        animator.Play("weaponSwing");
        UIManager.main.WeaponCooldown(cooldownDuration);
    }

    public void SwingAnimationFinished()
    {
        //Debug.Log("anim finished");
        //isSwinging = false;
    }


    public Vector3 GetDirection()
    {
        return aimingReticule.transform.position - transform.position;
    }

}
