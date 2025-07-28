using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Animator m_animator;
    public int maxHealth = 100;
    public int playerHealth;
    public Texture2D redTexture;

    private bool canHeal = true;
    private float healCD = 10f;
    void Start()
    {
        redTexture = MakeTex(1, 1, Color.red);
        playerHealth = maxHealth;
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        Heal();
        if (!canHeal)
        {
            healCD -= Time.deltaTime;
            if (healCD <= 0f)
            {
                canHeal = true;
                healCD = 10f;
            }
        }

        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
    private void OnGUI()
    {
        float healthBarWidth = 1000f;
        float healthBarHeight = 50f;

        GUI.Box(new Rect(10, 10, healthBarWidth, healthBarHeight), "");

        float healthPercentage = (float)playerHealth / maxHealth;
        GUIStyle hpStyle = new GUIStyle(GUI.skin.box);
        hpStyle.normal.background = redTexture;

        GUI.Box(new Rect(10, 10, healthBarWidth * healthPercentage, healthBarHeight), "", hpStyle);
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++) pix[i] = col;
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
    public void Heal()
    {
        if (Input.GetKeyDown("q") && canHeal)
        {
            if (canHeal)
            {
                playerHealth += 10;
                if (playerHealth > maxHealth)
                {
                    playerHealth = maxHealth;
                }
                canHeal = false;
            }
        }
    }
}

