using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(LineRenderer))]
public class PlayerAttackII : MonoBehaviour
{
    public float attackDistance = 1.2f;
    public float attackRadius = 0.4f;
    public float attackDuration = 0.2f;
    public int attackDamage = 1;
    public int circleSegments = 30;

    private bool isAttacking = false;
    private float attackTimer = 0f;
    private Renderer rend;
    private Color originalColor;
    private LineRenderer lineRenderer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = circleSegments + 1;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAttacking)
        {
            isAttacking = true;
            attackTimer = attackDuration;
            rend.material.color = Color.red;
            AttackEnemies();
            DrawAttackCircle();
            lineRenderer.enabled = true;
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                rend.material.color = originalColor;
                lineRenderer.enabled = false;
            }
        }
    }

    void AttackEnemies()
    {
        Vector3 center = transform.position + transform.forward * attackDistance;
        Collider[] hitColliders = Physics.OverlapSphere(center, attackRadius);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyBaseII enemy = col.GetComponent<EnemyBaseII>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);
                }
            }
        }
    }

    void DrawAttackCircle()
    {
        Vector3 center = transform.position + transform.forward * attackDistance;
        for (int i = 0; i <= circleSegments; i++)
        {
            float angle = i * Mathf.PI * 2 / circleSegments;
            float x = Mathf.Cos(angle) * attackRadius;
            float z = Mathf.Sin(angle) * attackRadius;
            Vector3 pos = center + new Vector3(x, 0, z);
            lineRenderer.SetPosition(i, pos);
        }
    }
}