using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKing : MonoBehaviour
{
    private Transform target;
    private SkeletonKingState state = SkeletonKingState.SPAWN;
    private CharacterAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMovement.main.transform;
        animator = GetComponent<CharacterAnimator>();
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCallback() {
        animator.Idle();
        state = SkeletonKingState.IDLE;
    }

    public void AttackDone() {

    }

    private void spawn() {
        state = SkeletonKingState.SPAWN;
        animator.PlayCustomAnimation("spawn");
    }
}

enum SkeletonKingState {
    SPAWN,
    IDLE,
    ENGAGE,
    ATTACK1,
    ATTACK2

}
