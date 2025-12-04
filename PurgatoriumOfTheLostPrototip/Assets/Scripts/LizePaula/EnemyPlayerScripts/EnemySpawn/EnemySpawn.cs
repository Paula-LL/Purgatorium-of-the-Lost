using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemySpawnPos1;
    public Transform enemySpawnPos2;
    public Transform enemySpawnPos3;
    public Transform enemySpawnPos4;
    public EnemiesKilledController rewardController;

    // Referencia opcional a la puerta para notificar
    public PuertaFSM puertaParaAbrir;

    private List<EnemigoBase> spawnedEnemies = new List<EnemigoBase>();
    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !hasSpawned)
        {
            hasSpawned = true;
            Invoke(nameof(EnemySpawner), 0);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        GameObject enemy1 = Instantiate(enemy, enemySpawnPos1.position, enemySpawnPos1.rotation);
        GameObject enemy2 = Instantiate(enemy, enemySpawnPos2.position, enemySpawnPos2.rotation);
        GameObject enemy3 = Instantiate(enemy, enemySpawnPos3.position, enemySpawnPos3.rotation);
        GameObject enemy4 = Instantiate(enemy, enemySpawnPos4.position, enemySpawnPos4.rotation);

        AddEnemyToList(enemy1);
        AddEnemyToList(enemy2);
        AddEnemyToList(enemy3);
        AddEnemyToList(enemy4);

        if (rewardController != null)
        {
            rewardController.RegisterSpawnedEnemies(spawnedEnemies);
        }

        StartCoroutine(CheckEnemiesStatus());
    }

    void AddEnemyToList(GameObject enemyObj)
    {
        EnemigoBase enemyBase = enemyObj.GetComponent<EnemigoBase>();
        if (enemyBase != null) spawnedEnemies.Add(enemyBase);
    }

    IEnumerator CheckEnemiesStatus()
    {
        while (spawnedEnemies.Count > 0)
        {
            for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
            {
                if (spawnedEnemies[i] == null)
                {
                    spawnedEnemies.RemoveAt(i);
                }
            }

            if (spawnedEnemies.Count == 0)
            {
                // Todos los enemigos han muerto
                if (rewardController != null)
                {
                    rewardController.OnAllSpawnedEnemiesDead();
                }

                // Notificar a la puerta para que se abra
                if (puertaParaAbrir != null)
                {
                    puertaParaAbrir.AbrirCuandoEnemigosMueren();
                }

                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    // Método público para que otras clases puedan verificar si ya spawnearon
    public bool EnemigosHanSpawned()
    {
        return hasSpawned;
    }
}