using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    private Transform targetTransform;
    private Vector2 targetDirection;

    private Rigidbody2D rb;

    private float stopTrackingDistance = 0.5f;

    [SerializeField]
    private Transform sprite;

    private float maxLifeTime = 10.0f;
    private float spawned;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawned = Time.time;
    }
    
    public void Init(Transform target, bool trackEnemy) {
        if (trackEnemy) {
            targetTransform = target;
        } else {
            targetDirection = target.position - transform.position;
        }
        rotateTowardsTarget();
    }

    public void Init(Vector2 target) {
        targetDirection = target - (Vector2)transform.position;
        rotateTowardsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTransform != null) {
            targetDirection = targetTransform.position - transform.position;

            if (Vector2.Distance(transform.position, targetTransform.position) < stopTrackingDistance) {
                targetTransform = null;
            }
        }

        if(Time.time - spawned > maxLifeTime) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        rb.velocity = targetDirection.normalized * speed;
    }

    private void rotateTowardsTarget() {
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        sprite.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}