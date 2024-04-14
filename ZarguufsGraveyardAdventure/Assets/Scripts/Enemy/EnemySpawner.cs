using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float rangeToSpawn;

    [SerializeField]
    private float rangeToDespawn;

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private int spawnCountAtOnce;

    [SerializeField]
    private int maxSpawned;

    [SerializeField]
    private List<GameObject> randomSpawnPrefab;

    [SerializeField]
    private float randomSpawnRadius;

    [SerializeField]
    private bool normalizeSpawnRadius;

    [Header("Gizmos")]
    [SerializeField]
    private bool showSpawnRange;

    [SerializeField]
    private bool showDespawnRange;

    [SerializeField]
    private bool showRandomSpawnRadius;

    private float lastSpawnTime;
    private List<GameObject> spawnList = new();
    private int spawnCount;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnDrawGizmos()
    {
        if (showDespawnRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, rangeToDespawn);
        }
        if (showSpawnRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, rangeToSpawn);
        }
        if (showRandomSpawnRadius)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, randomSpawnRadius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = new(PlayerMovement.main.transform.position.x, PlayerMovement.main.transform.position.y);
        Vector2 spawnerPos = new(transform.position.x, transform.position.y);

        if (spawnCount >= maxSpawned)
        {
            return;
        }

        if (Vector2.Distance(playerPos, spawnerPos) < rangeToSpawn)
        {
            if (Time.time - lastSpawnTime > spawnTime)
            {
                SpawnEnemies();
                lastSpawnTime = Time.time;
            }
        }

        if (Vector2.Distance(spawnerPos, playerPos) > rangeToDespawn)
        {
            spawnList.ForEach(x => Destroy(x));
            spawnCount = 0;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnCountAtOnce; i++)
        {
            GameObject prefab = randomSpawnPrefab[Random.Range(0, randomSpawnPrefab.Count)];

            GameObject instance = Instantiate(prefab);
            instance.transform.parent = transform;

            if (normalizeSpawnRadius)
            {
                instance.transform.localPosition = Random.insideUnitCircle.normalized * randomSpawnRadius;
            }
            else
            {
                instance.transform.localPosition = Random.insideUnitCircle * randomSpawnRadius;
            }
            spawnList.Add(instance);
            spawnCount++;
        }
    }
}
