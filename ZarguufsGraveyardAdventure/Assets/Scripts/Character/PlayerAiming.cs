using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField]
    private GameObject aimingReticule;

    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private SpriteRenderer weaponRenderer;

    [SerializeField]
    private Animator animator;


    private bool isSwinging = false;


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

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseButton 0");
            if (isSwinging) {
                return;
            }
            SwingWeapon();
        }
    }

    private void SwingWeapon() {
        isSwinging = true;
        Debug.Log("Swing");
        animator.Play("weaponSwing");
    }

    public void SwingAnimationFinished() {
        Debug.Log("anim finished");
        isSwinging = false;
    }


    public Vector3 GetDirection()
    {
        return aimingReticule.transform.position - transform.position;
    }

}
