using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RunTowardsTargetEnemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float targetRange = 0.1f;

    [SerializeField]
    private float aggroRange;

    [SerializeField]
    private float smellRange;

    [SerializeField]
    private bool hasSpawnAnimation;

    private Transform target;

    private Rigidbody2D rb;

    private CharacterAnimator animator;

    private NavMeshPath path;
    private bool navigationActive = true;
    private float navigationUpdateInterval = 0.1f;
    private int waypointIndex = 0;
    private float waypointDistanceCheckEpsilon = 0.1f;

    [SerializeField]
    private SpriteRenderer sprite;

    private bool running = true;
    private bool isKnockedBack = false;
    private float knockBackDuration = 0.2f;
    private float knockBackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMovement.main.transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<CharacterAnimator>();
        if (target == null)
        {
            target = PlayerMovement.main.transform;
        }

        if (!hasSpawnAnimation)
        {
            EnableNavigation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        knockBackTimer += Time.deltaTime;
        if (knockBackTimer > knockBackDuration)
        {
            knockBackTimer = 0f;
            isKnockedBack = false;
        }
    }

    public void KnockBack(float strength, float duration)
    {
        knockBackDuration = duration;
        rb.velocity = -rb.velocity * strength;
        isKnockedBack = true;
    }

    void FixedUpdate()
    {
        if (!navigationActive)
        {
            return;
        }

        if (isKnockedBack)
        {
            return;
        }
        if (!running)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        runTowardsWaypoint();

        if ((!HasLOSToTarget() && GetDistanceToTarget() > smellRange) || GetDistanceToTarget() > aggroRange)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isFinalWaypoint() && GetDistanceToTarget() < targetRange)
        {
            rb.velocity = Vector2.zero;
        }


        if (rb.velocity.magnitude > 0.1f)
        {
            animator.Walk();
        }
        else
        {
            animator.Idle();
        }

        Vector2 targetDirection = target.position - transform.position;
        if (targetDirection.x > 0.1f)
        {
            sprite.flipX = true;
        }
        else if (targetDirection.x < -0.1f)
        {
            sprite.flipX = false;
        }
    }

    public void StartRunning()
    {
        running = true;
    }

    public void StopRunning()
    {
        running = false;
    }

    public void EnableNavigation()
    {
        navigationActive = true;
        updatePathing();
    }

    public void DisableNavigation()
    {
        navigationActive = false;
    }

    public float GetDistanceToTarget()
    {
        if (path == null || path.corners == null || path.corners.Length == 0)
        {
            return float.MaxValue;
        }
        var distanceSum = 0.0f;
        var nextWaypoint = waypointIndex;
        distanceSum += Vector2.Distance(transform.position, path.corners[nextWaypoint]);
        nextWaypoint++;
        while (nextWaypoint < path.corners.Length)
        {
            distanceSum += Vector2.Distance(path.corners[nextWaypoint - 1], path.corners[nextWaypoint]);
            nextWaypoint++;
        }
        return distanceSum;
    }

    public bool HasLOSToTarget()
    {
        if (path == null || path.corners == null || path.corners.Length == 0)
        {
            return false;
        }
        return isFinalWaypoint();
    }

    private void updatePathing()
    {
        NavMeshPath newPath = new NavMeshPath();
        var sourcePos = transform.position;
        sourcePos.z = 0;
        var targetPos = target.position;
        targetPos.z = 0;
        var success = NavMesh.CalculatePath(sourcePos, targetPos, NavMesh.AllAreas, newPath);
        if (success)
        {
            path = newPath;
            waypointIndex = 0;
        }

        if (navigationActive)
        {
            Invoke("updatePathing", navigationUpdateInterval);
        }
    }

    private void runTowardsWaypoint()
    {
        if (path == null || path.corners == null || path.corners.Length == 0)
        {
            return;
        }
        updateWayPoint();

        var targetPosition = path.corners[waypointIndex];
        if (targetPosition != null)
        {
            rb.velocity = (targetPosition - transform.position).normalized * speed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void updateWayPoint()
    {
        var targetPosition = path.corners[waypointIndex];
        if (targetPosition != null)
        {
            if (waypointReached() && !isFinalWaypoint())
            {
                waypointIndex++;
            }
        }
    }

    private bool waypointReached()
    {
        var targetPosition = path.corners[waypointIndex];
        return Vector2.Distance(transform.position, targetPosition) < waypointDistanceCheckEpsilon;
    }

    private bool isFinalWaypoint()
    {
        return waypointIndex == path.corners.Length - 1;
    }
}
