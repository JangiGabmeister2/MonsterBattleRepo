using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStates { start, player, enemy, win, lose }
public enum enemyStates { highHealth, lowHealth, noHeals, finalMove }

public class StatusChangeAI : MonoBehaviour
{
    public Text actionText;

    private gameStates _game;
    private enemyStates _enemy;

    private FighterInfo _fighter;

    public void Start()
    {
        StartCoroutine(StartGame());
        _game = gameStates.start;
    }

    #region Game States
    IEnumerator StartGame()
    {
        _fighter.SetHealth();
        _enemy = enemyStates.highHealth;

        yield return new WaitForSeconds(5f);

        StartCoroutine(PlayerTurn());
    }
    public IEnumerator PlayerTurn()
    {
        yield return null;
    }
    public IEnumerator EnemyTurn()
    {
        yield return null;
    }
    public IEnumerator PlayerWin()
    {
        yield return null;
    }
    public IEnumerator PlayerLose()
    {
        yield return null;
    }
    #endregion
    #region Enemy States
    public IEnumerator HighHealth()
    {
        yield return null;
    }
    public IEnumerator LowHealth()
    {
        yield return null;
    }
    public IEnumerator NoHeals()
    {
        yield return null;
    }
    public IEnumerator FinalMove()
    {
        yield return null;
    }
    #endregion

    #region Damage/Heal
    public void Damage(int damageAmount)
    {
        if (_fighter.health - damageAmount < 0)
        {
            _fighter.health = 0;
        }
        else
        {
            _fighter.health -= damageAmount;
        }
        _fighter.SetHealth();
    }

    public void Heal(int healAmount)
    {
        if (_fighter.health + healAmount > _fighter.maxHealth)
        {
            _fighter.health = _fighter.maxHealth;
        }
        else
        {
            _fighter.health += healAmount;
        }
        _fighter.SetHealth();
    }
    #endregion
    
    public void OnDamageButton()
    {
            
    }

    public void OnHealButton()
    {

    }
}
