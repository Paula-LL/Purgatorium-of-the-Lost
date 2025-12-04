using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player_controller : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] Movement baseMovement = new Movement();
    Movement currentMovement;

    [Header("Vida")]
    public int maxHealth = 5;
    private int currentHealth;

    [Header("Muerte")]
    public string sceneOnDeath = "GameOver";
    public float deathDelay = 2f;

    [Header("SFX")]
    public AudioClip footstepSFX;
    public AudioClip deathSFX;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool isDashing = false;
    private float dashTimeLeft = 0f;

    private PlayerAttack playerAttack;
    private AudioSource audioSource;
    private Animator anim;

    public List<MovementModifier> modifierMovementList = new List<MovementModifier>();

    bool isDead = false;

    void Start()
    {
        currentMovement = new Movement(baseMovement);
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = footstepSFX;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isDead) return;

        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        Vector3 moveInput = Vector3.zero;

        Gamepad pad = Gamepad.current ?? (Gamepad.all.Count > 0 ? Gamepad.all[0] : null);
        if (pad != null)
        {
            Vector2 stick = pad.leftStick.ReadValue();
            moveInput = new Vector3(stick.x, 0, stick.y);
        }

        float x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        float z = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        Vector3 keyboardInput = new Vector3(x, 0, z);

        moveDirection = moveInput.sqrMagnitude > 0 ? moveInput.normalized : keyboardInput.normalized;
        anim.SetFloat("Speed", moveDirection.magnitude);

        if (!isDashing && moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, currentMovement.rotationSpeed * Time.deltaTime);
        }

        bool dashKeyboard = Input.GetKeyDown(KeyCode.LeftShift);
        bool dashGamepad = pad != null && pad.buttonEast.wasPressedThisFrame;

        if (!isDashing && (dashKeyboard || dashGamepad) && moveDirection.magnitude > 0)
        {
            StartDash();
        }

        float speed = isDashing ? currentMovement.dashSpeed : currentMovement.moveSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
                anim.SetBool("IsDashing", false);
            }
        }

        if (moveDirection.magnitude > 0.1f && !isDashing)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    void HandleAttack()
    {
        Gamepad pad = Gamepad.current;

        bool attackKeyboard = Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame;
        bool attackGamepad = pad != null && pad.buttonSouth.wasPressedThisFrame;

        if (attackKeyboard || attackGamepad)
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        if (playerAttack != null)
        {
            anim.SetBool("IsAttacking", true);
            playerAttack.TriggerAttack();
            Invoke(nameof(StopAttackAnimation), 2.0f);
        }
    }

    void StopAttackAnimation()
    {
        anim.SetBool("IsAttacking", false);
    }

    void StartDash()
    {
        isDashing = true;
        currentMovement = new Movement(baseMovement);
        ApplyMovementModifiers(currentMovement);
        dashTimeLeft = currentMovement.dashDuration;
        anim.SetBool("IsDashing", true);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Jugador recibe {amount} de daño. Vida actual: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        audioSource.Stop();
        audioSource.PlayOneShot(deathSFX);

        anim.SetBool("IsDead", true);

        // Desactivar controles
        controller.enabled = false;

        // Iniciar corrutina de muerte
        StartCoroutine(DeathRoutine());
    }

    System.Collections.IEnumerator DeathRoutine()
    {
        // Esperar el tiempo de la animación de muerte
        yield return new WaitForSeconds(deathDelay);

        // Cargar la escena asignada
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

    public Movement() { }

    public Movement(Movement movement)
    {
        moveSpeed = movement.moveSpeed;
        dashSpeed = movement.dashSpeed;
        dashDuration = movement.dashDuration;
        rotationSpeed = movement.rotationSpeed;
    }
}