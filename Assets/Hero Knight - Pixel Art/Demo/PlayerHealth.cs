using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Animator m_animator;
    public int maxHealth = 100;
    public int playerHealth;
    void Start()
    {
        playerHealth = maxHealth;
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Yokai"))
        {
            if (playerHealth > 0)
            {
                Debug.Log("Player collided with Yokai!");
                playerHealth -= 10;
                m_animator.SetTrigger("Hurt");
            }
            if (playerHealth <= 0)
            {
                m_animator.SetTrigger("Death");
                Debug.Log("Player is dead!");
            }
        }
    }
}

