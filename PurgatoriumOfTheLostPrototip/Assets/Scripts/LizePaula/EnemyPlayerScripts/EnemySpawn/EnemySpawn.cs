using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemySpawnPos;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player") {
            Invoke(nameof(EnemySpawner), 0);
            Destroy(gameObject);
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Collider Triggered");
        }
    }

    void EnemySpawner() {
        Instantiate(enemy, enemySpawnPos.position, enemySpawnPos.rotation);
        Debug.Log("Enemy Spawned");
    }
}
