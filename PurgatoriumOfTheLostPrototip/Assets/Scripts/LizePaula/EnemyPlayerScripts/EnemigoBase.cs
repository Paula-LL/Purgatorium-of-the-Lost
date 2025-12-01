using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class EnemigoBase : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float moveSpeed = 3f;

    [Header("Damage Settings")]
    [SerializeField] private float timeBetweenAttacks = 3f;
    [SerializeField] private int damagePerAttack = 1;

    [Header("Health Settings")]
    public int maxHealth = 3;
    private float currentHealth;

    private Transform player;
    private bool playerInRange = false;
    private float timeInRange = 0f;
    private float lastDamageTime = 0f;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    public void Awake()
    {
        EnemiesController.instance.enemyList.Add(this);
        Debug.Log("Enemies in list: " + EnemiesController.instance.enemyList.Count);
    }

    void Update()
    {
        FollowPlayer();

        if (playerInRange)
        {
            ProcessDamage();
        }
    }

    void FollowPlayer()
    {
        if (player != null)
        {

            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );


            transform.LookAt(player);
        }
    }

    void ProcessDamage()
    {
        timeInRange += Time.deltaTime;

        if (timeInRange >= lastDamageTime + timeBetweenAttacks)
        {
            DealDamageToPlayer();
            lastDamageTime = timeInRange;
        }
    }

    void DealDamageToPlayer()
    {
        if (player != null)
        {
            Player_controller playerScript = player.GetComponent<Player_controller>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(damagePerAttack);
            }
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
        EnemiesController.instance.KilledOpponent(this);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            timeInRange = 0f;
            lastDamageTime = timeBetweenAttacks;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            timeInRange = 0f;
        }
    }
}