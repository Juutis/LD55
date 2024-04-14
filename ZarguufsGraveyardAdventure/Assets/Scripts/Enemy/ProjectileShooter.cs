using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RunTowardsTargetEnemy))]
public class ProjectileShooter : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 3.0f;

    [SerializeField]
    private float attackInterval = 1.5f;

    [SerializeField]
    private Projectile projectile;

    [SerializeField]
    private Transform projectileRoot;

    [SerializeField]
    private Transform target;

    private RunTowardsTargetEnemy navigation;
    private float lastAttackTimestamp;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GetComponent<RunTowardsTargetEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack()) {
            attack();
        }
    }

    private bool canAttack() {
        return isWithinAttackRange() && Time.time > lastAttackTimestamp + attackInterval;
    }

    private void attack() {
        var newProj = Instantiate(projectile);
        newProj.transform.position = projectileRoot.position;
        newProj.Init(target, false);
        lastAttackTimestamp = Time.time;
    }

    private bool isWithinAttackRange() {
        return navigation.GetDistanceToTarget() < attackRange;
    }
}
