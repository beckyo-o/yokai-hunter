using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YokaiHPMechanics : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Transform player; // Assign this in the Inspector
    public float moveSpeed = 3f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
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
