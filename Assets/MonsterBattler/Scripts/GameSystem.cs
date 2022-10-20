using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStates { player, enemy, win, lose } //game states
public enum EnemyStates { attacking, healing} //enemy states

public class GameSystem : MonoBehaviour
{
    #region Variables
    [Header("Action Texts")]
    public Text enemyStatesText;
    public Text gameStatesText;

    [Header("Action Buttons")]
    public Button healButton;
    public Button damageButton;

    [Header("Current States")]
    public GameStates gameState;
    public EnemyStates enemyState;

    public bool isPlayerTurn;

    private PlayerFighter _player;
    private EnemyFighter _enemy;
    #endregion

    public void Start()
    {
        gameState = GameStates.player;
        enemyState = EnemyStates.attacking;

        NextEnemyState();
        NextGameState();

        isPlayerTurn = true;

        enemyStatesText.text = "Enemy State: Attack";
        gameStatesText.text = "Game State: Your Turn";
    }

    #region States
    public void NextGameState()
    {
        switch (gameState)
        {
            case GameStates.player:
                StartCoroutine(PlayerTurn());
                break;
            case GameStates.enemy:
                StartCoroutine(EnemyTurn());
                break;
            case GameStates.win:
                StartCoroutine(Win());
                break;
            case GameStates.lose:
                StartCoroutine(Lose());
                break;
            default:
                break;
        }
    }

    public void NextEnemyState()
    {
        switch (enemyState)
        {
            case EnemyStates.attacking:
                StartCoroutine(Attack());
                break;
            case EnemyStates.healing:
                StartCoroutine(Healng());
                break;
            default:
                break;
        }
    }

    #region Game States
    public IEnumerator PlayerTurn()
    {
        while (gameState == GameStates.player && isPlayerTurn)
        {
            healButton.enabled = true;
            damageButton.enabled = true;
        }
        yield return null;
    }

    public IEnumerator EnemyTurn()
    {
        while (gameState == GameStates.enemy && !isPlayerTurn)
        {

        }

        yield return null;
    }
    public IEnumerator Win()
    {
        yield return null;
    }

    public IEnumerator Lose()
    {
        yield return null;
    }
    #endregion
    #region Enemy States
    public IEnumerator Attack()
    {
        yield return null;
    }

    public IEnumerator Healng()
    {
        yield return null;
    }
    #endregion
    #endregion

    public IEnumerator DecideAction()
    {
        yield return null;
    }

    public void OnButtonClick()
    {
        healButton.enabled = false;
        damageButton.enabled = false;


    }

    public void EndGame()
    {
        Application.Quit();
    }
}
