using UnityEngine;

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

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!isDashing)
        {
            // Direcciones
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector3(x, 0, z).normalized;

            // Rotar hacia la dirección de movimiento
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Dash
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
        currentHealth -= amount;
        Debug.Log($"Jugador recibe {amount} de daño. Vida actual: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Jugador ha muerto");
        Destroy(gameObject);
    }
}