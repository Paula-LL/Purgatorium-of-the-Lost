using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    static EnemiesController enemiesController;
    public static EnemiesController instance
    {
        get
        {
            if (enemiesController == null) enemiesController = FindObjectOfType<EnemiesController>();
            return enemiesController;
        }
    }
    public List<EnemigoBase> enemyList = new List<EnemigoBase>();

    public GameObject cardReward;
    public Transform spawnPoint;

    void Start()
    {
        print(enemyList.Count);
    }

    public void KilledOpponent(EnemigoBase enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }

        print(enemyList.Count);

        if (AreOpponentsDead())
        {
            SpawnReward();
        }
    }

    public bool AreOpponentsDead()
    {
        if (enemyList.Count <= 0)
        {
            Debug.Log("Dead");

            return true;
        }
        else
        {
            Debug.Log("Alive");
            return false;
        }
    }

    void SpawnReward()
    {
        if (cardReward == null)
        {
            Debug.LogWarning("No reward prefab assigned!");
            return;
        }

        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;
        Instantiate(cardReward, pos, Quaternion.identity);

        Debug.Log("Reward spawned");
    }
}
