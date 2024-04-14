using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonKing : MonoBehaviour
{

    private float attackRange = 2.0f;



    private Transform target;
    private SkeletonKingState state = SkeletonKingState.SPAWN;
    private CharacterAnimator animator;
    private RunTowardsTargetEnemy navigation;

    private float idleTimer = 0f;

    [SerializeField]
    private Transform swordContainer;

    [SerializeField]
    private Transform handContainer;

    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Projectile bigProjectile;

    [SerializeField]
    private Transform projectileRoot;

    [SerializeField]
    private Transform spellRoot;

    [SerializeField]
    private SkeletonKingState[] availableAttacks;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMovement.main.transform;
        animator = GetComponent<CharacterAnimator>();
        navigation = GetComponent<RunTowardsTargetEnemy>();
        navigation.StopRunning();
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state) {
            case SkeletonKingState.SPAWN:
                spawnRoutine();
                break;
            case SkeletonKingState.IDLE:
                idleRoutine();
                break;
            case SkeletonKingState.ENGAGE:
                engageRoutine();
                break;
            case SkeletonKingState.ATTACK1:
                attackRoutine1();
                break;
            case SkeletonKingState.ATTACK2:
                attackRoutine2();
                break;
            case SkeletonKingState.ATTACK3:
                attackRoutine3();
                break;
            case SkeletonKingState.CAST1:
                castRoutine1();
                break;
            case SkeletonKingState.CAST2:
                castRoutine2();
                break;
            case SkeletonKingState.CAST3:
                castRoutine3();
                break;
        }
    }

    void LateUpdate() {
        if (state == SkeletonKingState.ATTACK1 || state == SkeletonKingState.ATTACK3) {
            Vector2 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90;
            swordContainer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (state == SkeletonKingState.CAST1 || state == SkeletonKingState.CAST3) {
            Vector2 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90;
            handContainer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void spawnRoutine() {
        animator.PlayCustomAnimation("spawn");
        navigation.StopRunning();
    }

    private void idleRoutine() {
        navigation.StopRunning();
        animator.Idle();
        if (idleTimer < Time.time) {
            state = SkeletonKingState.ENGAGE;
        }
    }

    private void engageRoutine() {
        navigation.StartRunning();
        animator.Walk();
        if (navigation.GetDistanceToTarget() < attackRange) {
            state = nextAttack();
        }
    }

    private void attackRoutine1() {
        navigation.StopRunning();
        animator.PlayCustomAnimation("attack1");
    }

    private void attackRoutine2() {
        navigation.StartRunning();
        animator.PlayCustomAnimation("attack2");
    }

    private void attackRoutine3() {
        navigation.StopRunning();
        animator.PlayCustomAnimation("attack3");
    }

    private void castRoutine1() {
        navigation.StopRunning();
        animator.PlayCustomAnimation("cast1");
    }

    private void castRoutine2() {
        navigation.StopRunning();
        animator.PlayCustomAnimation("cast2");
    }

    private void castRoutine3() {
        navigation.StopRunning();
        animator.PlayCustomAnimation("cast3");
    }

    public void SpawnCallback() {
        animator.Idle();
        state = SkeletonKingState.IDLE;
        idleTimer = Time.time + 0.25f;
    }

    public void AttackDone() {
        switch(state) {
            case SkeletonKingState.ATTACK1:
                idleTimer = Time.time + 0.5f;
                break;
            case SkeletonKingState.ATTACK2:
                idleTimer = Time.time + 0.8f;
                break;
            case SkeletonKingState.ATTACK3:
                idleTimer = Time.time + 0.2f;
                break;
            
            case SkeletonKingState.CAST1:
                idleTimer = Time.time + 0.6f;
                break;
            case SkeletonKingState.CAST2:
                idleTimer = Time.time + 0.3f;
                break;
            case SkeletonKingState.CAST3:
                idleTimer = Time.time + 0.6f;
                break;
        }
        state = SkeletonKingState.IDLE;
    }

    private void spawn() {
        state = SkeletonKingState.SPAWN;
    }

    public void ShootShockWave() {
        var newProj = Instantiate(bigProjectile);
        newProj.transform.position = projectileRoot.position;
        newProj.Init(target, false);
    }

    public void ShootProjectile() {
        var isHoming = state == SkeletonKingState.CAST3;
        var newProj = Instantiate(projectile);
        newProj.transform.position = projectileRoot.position;
        newProj.Init(target, isHoming);
    }

    private SkeletonKingState nextAttack() {
        var t = Random.Range(0, availableAttacks.Length);
        return availableAttacks[t];
    }
}

enum SkeletonKingState {
    SPAWN,
    IDLE,
    ENGAGE,
    ATTACK1,
    ATTACK2,
    ATTACK3,
    CAST1,
    CAST2,
    CAST3
}
