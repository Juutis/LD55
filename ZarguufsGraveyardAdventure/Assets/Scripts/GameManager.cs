using System.Collections;
using System.Collections.Generic;
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

    public void PlayerDie()
    {
        for (int index = aliveBosses.Count - 1; index >= 0; index -= 1)
        {
            aliveBosses[index].Despawn();
            aliveBosses.RemoveAt(index);
        }

        PlayerMovement.main.transform.position = playerSpawn.position;
        PlayerMovement.main.PlayerHealth.HealToFull();
        Debug.Log("You died!");
        Time.timeScale = 1f;
    }
}
