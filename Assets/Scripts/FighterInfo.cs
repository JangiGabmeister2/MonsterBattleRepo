using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
    public Text healthText, staminaText;

    [Header("Figher Status Info ")]
    public int health;
    public int maxHealth;
    public int stamina;
    public int maxStamina;

    public bool isDead = false;

    [Header("Heal and Attack")]
    public Button healButton;   
    public Button damageButton;
    
    public void Damage(int damageAmount)
    {
        if (health - damageAmount < 0)
        {
            health = 0;
        }
        else
        {
            health -= damageAmount;
        }
        healthText.text = $"Health: {health} / {maxHealth}";
    }

    public void Heal(int healAmount)
    {
        if (isDead == false)
        {
            if (health + healAmount > maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += healAmount;
            }
            healthText.text = $"Health: {health} / {maxHealth}";
        }
        else
        {
            healButton.enabled = false;
            damageButton.enabled = false;
        }
    }
}
