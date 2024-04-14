using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RunTowardsTargetEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private Transform target;

    private Rigidbody2D rb;

    private CharacterAnimator animator;

    private NavMeshPath path;
    private bool navigationActive = true;
    private float navigationUpdateInterval = 0.1f;
    private int waypointIndex = 0;
    private float waypointDistanceCheckEpsilon = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
        EnableNavigation();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() {
        runTowardsWaypoint();
        if (rb.velocity.magnitude > 0.1f) {
            animator.Walk();
        } else {
            animator.Idle();
        }
    }

    public void EnableNavigation() {
        navigationActive = true;
        updatePathing();
    }

    public void DisableNavigation() {
        navigationActive = false;
    }

    private void updatePathing() {
        NavMeshPath newPath = new NavMeshPath();
        var sourcePos = transform.position;
        sourcePos.z = 0;
        var targetPos = target.position;
        targetPos.z = 0;
        var success = NavMesh.CalculatePath(sourcePos, targetPos, NavMesh.AllAreas, newPath);
        if (success) {
            path = newPath;
            waypointIndex = 0;
        }

        if (navigationActive) {
            Invoke("updatePathing", navigationUpdateInterval);
        }
    }

    private void runTowardsWaypoint() {
        if (path == null || path.corners == null || path.corners.Length == 0) {
            return;
        }
        updateWayPoint();
        
        var targetPosition = path.corners[waypointIndex];
        if (targetPosition != null) {
            rb.velocity = (targetPosition - transform.position).normalized * speed;
        } else {
            rb.velocity = Vector3.zero;
        }
    }

    private void updateWayPoint() {
        var targetPosition = path.corners[waypointIndex];
        if (targetPosition != null) {
            if (waypointReached() && !isFinalWaypoint()) {
                waypointIndex++;
            }
        }
    }

    private bool waypointReached() {
        var targetPosition = path.corners[waypointIndex];
        return Vector2.Distance(transform.position, targetPosition) < waypointDistanceCheckEpsilon;
    }

    private bool isFinalWaypoint() {
        return waypointIndex == path.corners.Length - 1;
    }
}