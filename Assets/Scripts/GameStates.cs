using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public enum enemyStates //states for enemy depending on health and stamina
    {
        lowHealth,
        highHeath,
        lowStamina,
        highStamina
    }

    public enum gameStates //states for game
    {
        Start,
        PlayerTurn,
        EnemyTurn,
        Win,
        Lose
    }

    [SerializeField] enemyStates _enemy;
    [SerializeField] gameStates _game;

    #region NextState Classes
    public void NextEnemyState()
    {
        switch (_enemy)
        {
            case enemyStates.highHeath:
                StartCoroutine(HighHealthState());
                break;
            case enemyStates.lowHealth:
                StartCoroutine(LowHealthState());
                break;
            case enemyStates.lowStamina:
                StartCoroutine(LowStaminaState());
                break;
            default:
                break;
        }
    }

    public void NextGameState()
    {
        switch (_game)
        {
            case gameStates.Start:
                StartCoroutine(StartState());
                break;
            case gameStates.PlayerTurn:
                StartCoroutine(PlayerTurnState());
                break;
            case gameStates.EnemyTurn:
                StartCoroutine(EnemyTurnState());
                break;
            case gameStates.Win:
                StartCoroutine(WinState());
                break;
            case gameStates.Lose:
                StartCoroutine(LoseState());
                break;
            default:
                break;
        }
    }
    #endregion
    #region States
    #region Enemy States
    private IEnumerator HighHealthState()
    {
        yield return null;
    }
    private IEnumerator LowHealthState()
    {
        yield return null;
    }
    private IEnumerator LowStaminaState()
    {
        yield return null;
    }
    #endregion
    #region Game States
    private IEnumerator StartState()
    {
        yield return null;
    }
    private IEnumerator PlayerTurnState()
    {
        yield return null;
    }
    private IEnumerator EnemyTurnState()
    {
        yield return null;
    }
    private IEnumerator WinState()
    {
        yield return null;
    }
    private IEnumerator LoseState()
    {
        yield return null;
    }
    #endregion
    #endregion
}
