using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetHealth : MonoBehaviour
{
    public Text healthText;

    public void SetupHealth(FighterInfo fighter)
    {
        healthText.text = $"Health: {fighter.health} / {fighter.maxHealth}";
    }
}
