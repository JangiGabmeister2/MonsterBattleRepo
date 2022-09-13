using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FighterInfo))]
[RequireComponent(typeof(GameStates))]

public class GameSequence : MonoBehaviour
{
    private FighterInfo _fighter;
    private GameStates _state;

    public void Start()
    {
        _fighter.healthText.text = $"Health: {_fighter.health} / {_fighter.maxHealth}";
        _fighter.staminaText.text = $"Stamina: {_fighter.stamina} / {_fighter.maxStamina}";
        _state.NextGameState();
    }

    public void Update()
    {
        if (_fighter.health <= 0)
        {
            _fighter.isDead = true;
        }
    }
}
