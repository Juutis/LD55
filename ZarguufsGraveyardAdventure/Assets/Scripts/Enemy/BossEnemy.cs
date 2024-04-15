using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField]
    private PerkGroup perkGroupPrefab;

    private Pentagram sourcePentagram;
    public void Init(Pentagram pentagram)
    {
        sourcePentagram = pentagram;
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
        Destroy(gameObject);
    }

    public void Despawn()
    {
        sourcePentagram.Unlock();
        Destroy(gameObject);
    }
}
