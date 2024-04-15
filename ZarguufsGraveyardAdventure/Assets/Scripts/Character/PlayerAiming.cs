using System;
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
    private GameObject weaponObject;
    private SpriteRenderer weaponRenderer;

    [SerializeField]
    private int damage = 2;
    public int Damage { get { return damage; } }
    [SerializeField]
    private float knockbackStrength = 5f;
    public float KnockbackStrength { get { return knockbackStrength; } }
    [SerializeField]
    private float knockbackDuration = 0.2f;
    public float KnockbackDuration { get { return knockbackDuration; } }

    [SerializeField]
    private float cooldownDuration = 0.5f;
    private float cooldownTimer = 0f;

    private bool isOnCooldown = false;
    private bool isSwinging = false;

    private float startingSwingPos = 0.4f;
    private float targetSwingPos = 0.7f;

    private float swingDuration = 0.1f;
    private float swingTimer = 0f;

    private Vector2 weaponStartPosition;
    private Vector2 weaponTargetPosition;


    // Start is called before the first frame update
    void Start()
    {
        weaponRenderer = weaponObject.GetComponent<SpriteRenderer>();
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

        if (isSwinging)
        {
            swingTimer += Time.deltaTime;
            weaponRenderer.transform.localPosition = Vector2.Lerp(weaponStartPosition, weaponTargetPosition, swingTimer / swingDuration);
            if (swingTimer >= swingDuration)
            {
                isSwinging = false;
                swingTimer = 0f;
                weaponRenderer.transform.localPosition = weaponTargetPosition;
                weaponObject.SetActive(false);
            }
        }

        if (Input.GetMouseButton(0))
        {
            //Debug.Log("MouseButton 0");
            if (isSwinging || isOnCooldown)
            {
                return;
            }
            SwingWeapon();
        }
    }

    private void SwingWeapon()
    {
        isSwinging = true;
        weaponStartPosition = new Vector2(startingSwingPos, 0f);
        weaponRenderer.transform.localPosition = weaponStartPosition;
        weaponTargetPosition = new Vector2(targetSwingPos, 0f);
        weaponObject.SetActive(true);
        isOnCooldown = true;
        cooldownTimer = 0f;
        UIManager.main.WeaponCooldown(cooldownDuration);
        SoundManager.main.PlaySound(GameSoundType.WeaponSwing);
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

    internal void IncreaseWeaponReach(float value)
    {
        targetSwingPos += value;
    }
    internal void IncreaseWeaponKnockback(float value)
    {
        knockbackStrength += value;
    }

    internal void IncreaseWeaponDamage(float value)
    {
        damage += Mathf.CeilToInt(value);
    }
}
