using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private PerkGroup perkGroupPrefab;

    [SerializeField]
    private BossType bossType;
    public BossType BossType { get { return bossType; } }

    [SerializeField]
    public Animator animator;

    private Pentagram sourcePentagram;
    public void Init(Pentagram pentagram)
    {
        sourcePentagram = pentagram;
        MusicManager.main.SwitchMusic(MusicType.Boss);
    }
    public void Die()
    {
        if (perkGroupPrefab != null)
        {
            PerkGroup group = Instantiate(perkGroupPrefab, transform.parent);
            sourcePentagram?.Unlock();
            group.transform.position = sourcePentagram?.PerkSpawnPosition?.position ?? transform.position;
            group.Init();
        }
        MusicManager.main.SwitchMusic(MusicType.Game);
        if (bossType == BossType.BoneBoss)
        {
            GameManager.main.AddRecord(GameRecordType.KillSkeletonBoss, 1);
        }
        else if (bossType == BossType.DemonBoss)
        {
            GameManager.main.AddRecord(GameRecordType.KillDemonBoss, 1);
        }
        else if (bossType == BossType.NoseBoss)
        {
            GameManager.main.AddRecord(GameRecordType.KillNoseBoss, 1);
        }

        SkeletonKing skeletonKing = GetComponent<SkeletonKing>();
        if (skeletonKing != null)
        {
            skeletonKing.enabled = false;
        }
        RunTowardsTargetEnemy runTowards = GetComponent<RunTowardsTargetEnemy>();
        if (runTowards != null)
        {
            runTowards.enabled = false;
        }
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.enabled = false;
        }
        animator.Play("bossDie");
        Debug.Log("Boss animation started");
    }


    public void DieAnimationFinished()
    {
        Debug.Log("Boss animation finished");
        if (bossType == BossType.NoseBoss)
        {
            GameManager.main.TheEnd();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Despawn()
    {
        sourcePentagram.Unlock();
        Destroy(gameObject);
    }
}
