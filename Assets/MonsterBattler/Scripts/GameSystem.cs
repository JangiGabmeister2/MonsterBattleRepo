using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStates { setup, player, enemy, win, lose } //game states
public enum EnemyStates { neutral, attacking, healing, blocking } //enemy states

[RequireComponent(typeof(Enemy))]
public class GameSystem : MonoBehaviour
{
    #region Variables
    [Header("Fighters")]
    public GameObject player;
    public GameObject enemy;

    [Header("Action Text")]
    public Text actionText;

    [Header("Fighter Placements")]
    public Transform playerPosition;
    public Transform enemyPosition;

    [Header("Health Texts")]
    public SetHealth playerHealth;
    public SetHealth enemyHealth;

    [Header("Action Buttons")]
    public Button healButton;
    public Button damageButton;

    private FighterInfo _playerFighter;
    private FighterInfo _enemyFighter;

    private bool isPlayerDead;
    private bool isEnemyDead;

    [Header("Current States")]
    public GameStates state;
    public EnemyStates enemyState;
    #endregion

    public void Start()
    {
        state = GameStates.setup; //sets game state to setting up info and fighter prefabs
        enemyState = EnemyStates.neutral; //sets enemy state to neutral
        StartCoroutine(SetupFight());
    }

    #region States
    #region Game States
    public IEnumerator SetupFight()
    {
        GameObject playerGO = Instantiate(player, playerPosition); //instantiates playe rprefab
        _playerFighter = playerGO.GetComponent<FighterInfo>();

        GameObject enemyGO = Instantiate(enemy, enemyPosition); //instantiates enemy prefab
        _enemyFighter = enemyGO.GetComponent<FighterInfo>();

        actionText.text = "Player   VS   Monster"; //sets action text

        playerHealth.SetupHealth(_playerFighter); //sets player and enemy healths
        enemyHealth.SetupHealth(_enemyFighter);

        healButton.enabled = false; //disables buttons before match
        damageButton.enabled = false;

        yield return new WaitForSeconds(5f); //waits for 5 seconds

        state = GameStates.player; //switches game state to player's turn
        StartCoroutine(PlayerTurn());
    }

    public IEnumerator PlayerTurn()
    {
        while (state == GameStates.player)
        {
            actionText.text = "Choose an action...";
            healButton.enabled = true; //if it is player's turn, buttons become enabled, so player can click on them
            damageButton.enabled = true;
        }

        healButton.enabled = false; //after player's turn, buttons become disabled, to prevent cheating
        damageButton.enabled = false;

        isEnemyDead = _enemyFighter.Damage(_playerFighter.damage); //tests if damage unto enemy killed it

        if (isEnemyDead)
        {
            state = GameStates.win; //if enemy is dead, player wins game 
        }
        else
        {
            state = GameStates.enemy; //or else enemy's turn next
        }

        yield return new WaitForSeconds(5f);
    }
    public IEnumerator EnemyTurn()
    {
        while (state == GameStates.enemy)
        {
            actionText.text = "The Monster is choosing an action...";
            StartCoroutine(ChooseAction()); //starts decision making time - choose an action depending on their health status
            switch (enemyState)
            {
                case EnemyStates.attacking: //if attacking, attacks player
                    actionText.text = "The Monster attacked you!";
                    break;
                case EnemyStates.healing: //if healing, heals self
                    actionText.text = "The Monster healed!";
                    break;
                case EnemyStates.blocking: //if blocking, blocks next player attack, if they attack, expires after player's turn
                    actionText.text = "The Monster is blocking!";
                    break;
                default:
                    break;
            }
        }

        isPlayerDead = _playerFighter.Damage(_enemyFighter.damage); //checks if player is killed

        if (isPlayerDead)
        {
            state = GameStates.lose; //if player is dead, player loses game
        }
        else
        {
            state = GameStates.player; //else player's turn next
        }

        yield return new WaitForSeconds(5f);
    }
    public IEnumerator PlayerWin()
    {
        while(state == GameStates.win)
        {
            actionText.text = "You have won! Game will close in 5 seconds.";
        }
        yield return new WaitForSeconds(5f);
        EndGame();
    }
    public IEnumerator PlayerLose()
    {
        while (state == GameStates.lose)
        {
            actionText.text = "You have lost! Game will close in 5 seconds.";
        }
        yield return new WaitForSeconds(5f);
        EndGame();
    }
    #endregion
    #region Enemy States
    public IEnumerator Blocking()
    {
        while (enemyState == EnemyStates.blocking)
        {
            _enemyFighter.Block(_playerFighter.damage, _playerFighter.damage);
        }
        yield return null;
    }
    public IEnumerator Attacking()
    {
        while (enemyState == EnemyStates.attacking)
        {
            _enemyFighter.Damage(_enemyFighter.damage);
        }
        yield return null;
    }
    public IEnumerator Healing()
    {
        yield return null;
    }
    #endregion
    #endregion

    IEnumerator ChooseAction()
    {
        if (_enemyFighter.health >= (_enemyFighter.maxHealth / 2)) //if enemy health >= 50%, starts attacking
        {
            StartCoroutine(Attacking());
        }
        else //if enemy health < 50%, starts healing
        {
            StartCoroutine(Healing());
        }

        if (_playerFighter.health >= (_playerFighter.maxHealth / 2) && _enemyFighter.health < (_enemyFighter.maxHealth / 2)) //if enemy health < 50% and player health > 50%
        {
            StartCoroutine(Blocking()); //then starts blocking
        }
        yield return null;
    }

    public void OnDamage()
    {
        if (state != GameStates.player)
        {
            return;
        }
    }

    public void OnHeal()
    {
        if (state != GameStates.player)
        {
            return;
        }
    }

    public void OnBlock()
    {
        if (state != GameStates.player)
        {
            return;
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
