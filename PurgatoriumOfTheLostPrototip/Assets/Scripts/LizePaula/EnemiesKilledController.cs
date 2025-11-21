using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesKilledController : MonoBehaviour
{
   List<GameObject> listOfOpponents = new List<GameObject>();

    void Start()
    {
        listOfOpponents.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        print(listOfOpponents.Count);
    }

    public void KilledOpponent(GameObject enemy)
    {
        if (listOfOpponents.Contains(enemy))
        {
            listOfOpponents.Remove(enemy);
        }

        print(listOfOpponents.Count);
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
}
