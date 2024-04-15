using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTriggerSpawner : MonoBehaviour
{
    [SerializeField]
    private List<Trigger> spawnTriggers;

    [SerializeField]
    private List<Trigger> despawnTriggers;

    [SerializeField]
    private List<Transform> spawnTransforms;

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private int spawnCountAtOnce;
    [SerializeField]
    private int itemCountAtOnce = 6;

    [SerializeField]
    private int maxSpawned;

    [SerializeField]
    private List<GameObject> randomSpawnPrefab;

    [SerializeField]
    private float randomSpawnRadius;

    [SerializeField]
    private bool normalizeSpawnRadius;

    private float lastSpawnTime;
    private List<GameObject> spawnList = new();
    private int spawnCount;
    private int killed;
    private bool spawnStarted = false;
    private bool spawnEnded = false;

    [SerializeField]
    private InventoryItemConfig item;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTriggers.Any(x => x.InsideTrigger()))
        {
            spawnStarted = true;
        }
        else
        {
            spawnStarted = false;
        }

        /*if (despawnTriggers.Any(x => x.InsideTrigger()))
        {
            spawnStarted = false;
            spawnList.ForEach(x => Destroy(x));
            spawnCount = 0;
        }*/

        if (killed >= spawnCount)
        {
            killed = 0;
            spawnCount = 0;
        }

        if (spawnCount >= maxSpawned)
        {
            return;
        }

        if (spawnStarted)
        {
            if (Time.time - lastSpawnTime > spawnTime)
            {
                SpawnEnemies();
                lastSpawnTime = Time.time;
            }
        }
    }

    private void ShuffleIndexes(List<int> indexList)
    {
        System.Random r = new System.Random();
        indexList.Sort((x, y) => r.Next(-1, 1));
    }

    private void SpawnEnemies()
    {
        List<int> indexesWithItems = new();
        for (int i = 0; i < spawnCountAtOnce; i++)
        {
            indexesWithItems.Add(i);
        }
        ShuffleIndexes(indexesWithItems);
        while (indexesWithItems.Count > itemCountAtOnce)
        {
            indexesWithItems.Remove(indexesWithItems.Count - 1);
        }
        Debug.Log($"{indexesWithItems.Count} skeletons should have item! ({itemCountAtOnce})");
        for (int i = 0; i < spawnCountAtOnce; i++)
        {
            Transform randomSpawnLocation = spawnTransforms[Random.Range(0, spawnTransforms.Count)];
            GameObject prefab = randomSpawnPrefab[Random.Range(0, randomSpawnPrefab.Count)];


            GameObject instance = Instantiate(prefab);
            instance.transform.parent = randomSpawnLocation;

            EnemyHealth enemyHealth = instance.GetComponent<EnemyHealth>();
            enemyHealth.AddDeathCallback(delegate
            {
                killed += 1;
            });
            if (indexesWithItems.Contains(i))
            {
                enemyHealth.AddForcedDrop(item);
                Debug.Log($"added forced drop to {instance}");
            }

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
