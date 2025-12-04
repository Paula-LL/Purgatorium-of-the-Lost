using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledController : MonoBehaviour
{
    List<EnemigoBase> listOfOpponents = new List<EnemigoBase>();

    public GameObject cardReward;
    public Transform spawnPoint;

    public void RegisterSpawnedEnemies(List<EnemigoBase> enemies)
    {
        listOfOpponents.Clear();
        listOfOpponents.AddRange(enemies);
    }

    public void OnAllSpawnedEnemiesDead()
    {
        SpawnReward();
    }

    void Start()
    {
        if (listOfOpponents.Count == 0)
        {
            listOfOpponents.AddRange(EnemigoBase.enemyList);
        }
    }

    public void KilledOpponent(EnemigoBase enemy)
    {
        if (listOfOpponents.Contains(enemy))
        {
            listOfOpponents.Remove(enemy);
        }

        if (AreOpponentsDead())
        {
            SpawnReward();
        }
    }

    public bool AreOpponentsDead()
    {
        return listOfOpponents.Count <= 0;
    }

    void SpawnReward()
    {
        if (cardReward == null) return;

        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(cardReward, pos, Quaternion.identity);
    }
}