using System.Collections;
using UnityEngine;

public class YokaiHPMechanics : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Transform player;
    public float moveSpeed = 3f;
    public float stopDistance = 1.2f;
    public float chargeSpeed = 8f;
    public float chargeDelay = 3f;
    public float chargeDuration = 3f;
    public float chargeCooldown = 3f;
    public int damageAmount = 10;

    private bool isCharging = false;
    private bool isOnCooldown = false;
    private Animator m_animator;

    void Start()
    {
        currentHealth = maxHealth;
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || isCharging || isOnCooldown) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            Vector3 dir = (player.position - transform.position).normalized;
            Vector3 move = new Vector3(dir.x, 0f, 0f);
            transform.position += move * moveSpeed * Time.deltaTime;
        }
        else
        {
            StartCoroutine(Charge());
        }
    }

    IEnumerator Charge()
    {
        isCharging = true;
        yield return new WaitForSeconds(chargeDelay);

        Vector3 direction = (player.position - transform.position).normalized;
        float timer = 0f;

        while (timer < chargeDuration)
        {
            transform.position += new Vector3(direction.x, 0f, 0f) * chargeSpeed * Time.deltaTime;
            timer += Time.deltaTime;
            yield return null;
        }

        isCharging = false;
        isOnCooldown = true;
        yield return new WaitForSeconds(chargeCooldown);
        isOnCooldown = false;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (!isCharging) return;

        if (col.gameObject.CompareTag("Player"))
        {
            PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.playerHealth -= damageAmount;
                Animator pa = col.gameObject.GetComponent<Animator>();
                if (pa != null)
                    pa.SetTrigger("Hurt");
            }

            isCharging = false;
            isOnCooldown = true;
            StopAllCoroutines();
            StartCoroutine(CooldownOnly());
        }
    }

    IEnumerator CooldownOnly()
    {
        yield return new WaitForSeconds(chargeCooldown);
        isOnCooldown = false;
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