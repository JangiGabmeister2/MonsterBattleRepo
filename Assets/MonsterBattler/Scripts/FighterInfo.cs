using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FighterInfo : MonoBehaviour
{
    [Header("Health Information")]
    public int health;
    public int maxHealth;

    [Header("Amounts")]
    public int heal;
    public int damage;

    public bool Damage(int damageAmount)
    {
        if (health - damageAmount < 0)
        {
            health = 0;
            return true;
        }
        else
        {
            health -= damageAmount;
            return false;
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

    public void Block(int blockAmount, int damage)
    {
        blockAmount -= damage;
    }
}
