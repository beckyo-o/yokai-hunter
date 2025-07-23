using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool m_attack = false;
    private int m_currentAttack = 0;
    private float m_attackCurrentTime;
    private float m_attackDuration = 8.0f / 14.0f;
    private Animator m_animator;
    private float m_timeSinceAttack = 0.0f;
    private bool m_attackHasHit = false; // Add this flag

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks attack duration
        if (m_attack)
            m_attackCurrentTime += Time.deltaTime;

        // Disable attacking if timer extends duration
        if (m_attackCurrentTime > m_attackDuration)
        {
            m_attack = false;
            m_attackCurrentTime = 0f;
            m_attackHasHit = false; // Reset flag after attack ends
        }

        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f)
        {
            m_currentAttack++;

            if (m_currentAttack > 3)
                m_currentAttack = 1;

            m_animator.SetTrigger("Attack" + m_currentAttack);

            m_attack = true;
            m_attackCurrentTime = 0f; // Reset timer when attack starts
            m_attackHasHit = false;   // Reset hit flag for new attack

            m_timeSinceAttack = 0.0f;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Yokai") && m_attack && !m_attackHasHit)
        {
            Debug.Log("Player attacked Yokai!");
            YokaiHPMechanics yokaiHP = col.gameObject.GetComponent<YokaiHPMechanics>();
            if (yokaiHP != null)
            {
                yokaiHP.TakeDamage(10);
                m_attackHasHit = true; // Only allow one hit per attack
            }
        }
    }
}
