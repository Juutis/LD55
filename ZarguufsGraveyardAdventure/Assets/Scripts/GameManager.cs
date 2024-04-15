using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    void Awake()
    {
        main = this;
    }
    [SerializeField]
    private Transform playerSpawn;
    private List<BossEnemy> aliveBosses = new();
    public void RegisterBoss(BossEnemy boss)
    {
        aliveBosses.Add(boss);
    }

    [SerializeField]
    private List<GameRecord> gameRecords = new();
    public List<GameRecord> GameRecords { get { return gameRecords; } }

    public void AddRecord(GameRecordType type, int count)
    {
        GameRecord record = gameRecords.FirstOrDefault(record => record.Type == type);
        if (record != null)
        {
            record.Add(count);
            //            Debug.Log($"Added {count} to record {record.Name}! Now at {record.Value}.");
        }
    }

    void Start()
    {
        Time.timeScale = 0f;
        MusicManager.main.StartMusic(MusicType.Menu);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        MusicManager.main.SwitchMusic(MusicType.Game);
        UIManager.main.TurnOnUI();
    }

    public void PlayerDie()
    {
        for (int index = aliveBosses.Count - 1; index >= 0; index -= 1)
        {
            if (aliveBosses[index] != null) {
                aliveBosses[index].Despawn();
                aliveBosses.RemoveAt(index);
            }
        }
        MusicManager.main.SwitchMusic(MusicType.Game);
        PlayerMovement.main.transform.position = playerSpawn.position;
        PlayerMovement.main.PlayerHealth.HealToFull();
        Debug.Log("You died!");
        Time.timeScale = 1f;
    }

    public void TheEnd()
    {
        Time.timeScale = 0f;
        UITheEnd.main.Open();
    }
}

public enum GameRecordType
{
    None,
    CollectGold,
    CollectBone,
    CollectEye,
    CollectEctoplasm,
    CollectHeart,
    KillSkeleton,
    KillZombie,
    KillGhost,
    Summon,
    KillSkeletonBoss,
    KillDemonBoss,
    KillNoseBoss,
    PlayerDeath
}

[System.Serializable]
public class GameRecord
{
    public GameRecordType Type;
    public string Name;
    public string Description;
    public int Value;
    public Sprite Sprite;

    public void Add(int count)
    {
        Value += count;
    }
}