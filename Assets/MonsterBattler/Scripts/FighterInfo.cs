using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterInfo : MonoBehaviour
{
    [Header("Health Information")]
    public Text healthText;
    public int health, maxHealth;

    public void SetHealth()
    {
        healthText.text = $"Health: {health} / {maxHealth}";
    }
}
