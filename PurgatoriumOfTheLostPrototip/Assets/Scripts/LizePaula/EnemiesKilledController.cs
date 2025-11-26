using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledController : MonoBehaviour
{
   List<EnemigoBase> listOfOpponents = new List<EnemigoBase>();

    public GameObject cardReward;
    public Transform spawnPoint;

    void Start()
    {
        listOfOpponents.AddRange(EnemigoBase.enemyList);
        print(listOfOpponents.Count);
    }

    public void KilledOpponent(EnemigoBase enemy)
    {
        if (listOfOpponents.Contains(enemy))
        {
            listOfOpponents.Remove(enemy);
        }

        print(listOfOpponents.Count);

        if (AreOpponentsDead())
        {
            SpawnReward();
        }
    }

    public bool AreOpponentsDead()
    {
        if (listOfOpponents.Count <= 0)
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
