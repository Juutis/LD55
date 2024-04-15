using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject enemySprite;
    [SerializeField]
    private RunTowardsTargetEnemy moveComponent;
    [SerializeField]
    private EnemyDamage enemyDamage;

    // Start is called before the first frame update
    void Start()
    {
        enemySprite.SetActive(false);
        enemyDamage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFinished()
    {
        Invoke("StartMoving", 0.5f);
    }

    private void StartMoving()
    {
        enemySprite.SetActive(true);
        moveComponent.EnableNavigation();
        enemyDamage.enabled = true;
        Destroy(gameObject);
    }
}
