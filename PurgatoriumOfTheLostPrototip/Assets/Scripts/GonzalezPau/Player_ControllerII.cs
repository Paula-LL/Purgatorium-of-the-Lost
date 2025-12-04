using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Player_ControllerII : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float rotationSpeed = 10f;

    [Header("Vida")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Muerte")]
    public string sceneOnDeath = "GameOver";
    public float deathDelay = 2f; // Tiempo de espera antes de cambiar escena

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private bool isDead = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
    }

    void HandleMovement()
    {
        if (!isDashing)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector3(z, 0, -x).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftShift) && moveDirection.magnitude > 0)
            {
                StartDash();
            }
        }
        else
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }

        float speed = isDashing ? dashSpeed : moveSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Jugador recibe {amount} de daño. Vida actual: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Jugador ha muerto");

        // Desactivar controles pero mantener el objeto para animaciones
        controller.enabled = false;

        // Iniciar corrutina para esperar antes de cambiar escena
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Aquí puedes activar cualquier animación de muerte si la tienes
        Debug.Log("Mostrando animación de muerte...");

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(deathDelay);

        // Cambiar a la escena asignada
        if (!string.IsNullOrEmpty(sceneOnDeath))
        {
            SceneManager.LoadScene(sceneOnDeath);
        }
        else
        {
            Debug.LogWarning("No se ha asignado nombre de escena para cargar al morir");
            Destroy(gameObject);
        }
    }
}