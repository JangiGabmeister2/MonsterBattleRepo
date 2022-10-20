using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FighterInfo : MonoBehaviour
{
    [Header("Health Information")]
    public int health;
    public int maxHealth;

    public Text healthText;

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
    }

    public void Heal(int healAmount)
    {
        if (health + healAmount > maxHealth)
        {
            health = maxHealth;

        }
        else
        {
            health += healAmount;
        }
    }

    public void Update()
    {
        healthText.text = $"Health: {health} / {maxHealth}";
    }
}
