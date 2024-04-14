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
        }
    }

    void LateUpdate() {
        if (state == SkeletonKingState.ATTACK1) {
            Vector2 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90;
            swordContainer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        navigation.StopRunning();
        animator.PlayCustomAnimation("attack2");
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
        }
        state = SkeletonKingState.IDLE;
    }

    private void spawn() {
        state = SkeletonKingState.SPAWN;
    }

    private SkeletonKingState prevAttack = SkeletonKingState.ATTACK2;

    private SkeletonKingState nextAttack() {
        if (prevAttack == SkeletonKingState.ATTACK1) {
            prevAttack = SkeletonKingState.ATTACK2;
            return SkeletonKingState.ATTACK2;
        } else {
            prevAttack = SkeletonKingState.ATTACK1;
            return SkeletonKingState.ATTACK1;
        }
    }
}

enum SkeletonKingState {
    SPAWN,
    IDLE,
    ENGAGE,
    ATTACK1,
    ATTACK2

}
