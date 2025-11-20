using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_controller;

[RequireComponent(typeof(CharacterController))]
public class Player_controller : MonoBehaviour
{
    [Header("Movimiento")]

    [SerializeField] Movement baseMovement = new Movement();
    Movement currentMovement;
    [Header("Vida")]
    public int maxHealth = 5;
    private int currentHealth;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;

    public List<MovementModifier> modifierMovementList = new List<MovementModifier>();

    void Start()
    {
        currentMovement = new Movement(baseMovement);
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentMovement.rotationSpeed * Time.deltaTime);
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

        float speed = isDashing ? currentMovement.dashSpeed : currentMovement.moveSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    void StartDash()
    {
        isDashing = true;
        currentMovement = new Movement(baseMovement);
        ApplyMovementModifiers(currentMovement);

        dashTimeLeft = currentMovement.dashDuration;
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

    internal void AddModifier(MovementModifier cardsBuff)
    {
        modifierMovementList.Add(cardsBuff);
    }

    void ApplyMovementModifiers(Movement m)
    {
        foreach (MovementModifier modifier in modifierMovementList)
        {
            modifier.ApplyMovementModifier(m);
        }
    }
}

[System.Serializable]
public class Movement
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float rotationSpeed = 10f;

    public Movement()
    {
        this.moveSpeed = 5f;
        this.dashSpeed = 20f;
        this.dashDuration = 0.1f;
        this.rotationSpeed = 10f;
    }

    public Movement(float moveSpeed, float dashSpeed, float dashDuration, float rotationSpeed)
    {
        this.moveSpeed = 5f;
        this.dashSpeed = 20f;
        this.dashDuration = 0.1f;
        this.rotationSpeed = 10f;
    }

    public Movement(Movement movement)
    {
        moveSpeed = movement.moveSpeed;
        dashSpeed = movement.dashSpeed;
        dashDuration = movement.dashDuration;
        rotationSpeed = movement.rotationSpeed;
    }
}


