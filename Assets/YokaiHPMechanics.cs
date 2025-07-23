using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokaiHPMechanics : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Transform player; // Assign this in the Inspector
    public float moveSpeed = 3f;
    public float stopDistance = 1.2f;
    public float chargeSpeed = 8f;
    public float chargeDelay = 0.5f;
    public int damageAmount = 10;

    private bool isCharging = false;
    private bool canCharge = true;
    private Animator m_animator;

    void Start()
    {
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null && canCharge)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (!isCharging)
            {
                // Move towards player until within stopDistance
                if (distanceToPlayer > stopDistance)
                {
                    Vector3 direction = (player.position - transform.position).normalized;
                    Vector3 horizontalDirection = new Vector3(direction.x, 0f, 0f);
                    transform.position += horizontalDirection * moveSpeed * Time.deltaTime;
                }
                else
                {
                    // Start charging after a short delay
                    StartCoroutine(ChargeAtPlayer());
                }
            }
        }
        Debug.Log(isCharging);
    }

    IEnumerator ChargeAtPlayer()
    {
        isCharging = true;
        yield return new WaitForSeconds(chargeDelay);

        // Charge towards player
        Vector3 direction = (player.position - transform.position).normalized;
        float chargeTime = 0.3f; // Duration of charge
        float elapsed = 0f;

        while (elapsed < chargeTime)
        {
            transform.position += new Vector3(direction.x, 0f, 0f) * chargeSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isCharging = false;
        canCharge = false;
        yield return new WaitForSeconds(0.5f);
        canCharge = true;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (isCharging && col.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.playerHealth -= damageAmount;
                Animator playerAnimator = col.gameObject.GetComponent<Animator>();
                if (playerAnimator != null)
                    playerAnimator.SetTrigger("Hurt");
            }
            isCharging = false;
            canCharge = false;
            StartCoroutine(ChargeCooldown());
        }
    }

    IEnumerator ChargeCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canCharge = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
