using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum GameStates { player, enemy, win, lose } //game states
public enum EnemyStates { neutral, attack, heal, shield, death } //enemy states

public class GameSystem : MonoBehaviour
{
    #region Variables
    [Header("Action Texts")]
    //texts to let player know the game and enemy states
    public Text enemyStatesText;
    public Text gameStatesText;

    [Header("Action Buttons")]
    //player buttons, disabled interactability when pressed
    public Button healButton;
    public Button damageButton;
    public Button blockButton;

    [Header("Current States")]
    //lets player know game and enemy states in inspector
    public GameStates gameState;
    public EnemyStates enemyState;

    //fighter references
    private PlayerFighter _player;
    private EnemyFighter _enemy;
    #endregion

    public void Start()
    {
        _player = GetComponent<PlayerFighter>();
        _enemy = GetComponent<EnemyFighter>();

        //sets game and enemy states
        gameState = GameStates.player;
        enemyState = EnemyStates.neutral;

        //swtiches states
        NextEnemyState();
        NextGameState();

        enemyStatesText.text = "Enemy State: Neutral";
        gameStatesText.text = "Game State: Your Turn";
    }

    #region Next States
    public void NextGameState()
    {
        switch (gameState)
        {
            //player = player's turn
            //enemy = enemy's turn
            //win = enemy dies, player wins
            //lose = player dies, enemy wins
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
            //neutral does nothing
            //attack = attacks player
            //heal = heals enemy health
            //shield = blocks player's next turn
            //death = enemy dies = player wins
            case EnemyStates.neutral:
                //does nothing
                break;
            case EnemyStates.attack:
                StartCoroutine(Attack());
                break;
            case EnemyStates.heal:
                StartCoroutine(Healing());
                break;
            case EnemyStates.shield:
                StartCoroutine(Blocking());
                break;
            case EnemyStates.death:
                StartCoroutine(Win());
                break;
            default:
                break;
        }
    }
    #endregion
    #region After Player Actions
    public void OnHeal()
    {
        //disables buttons, prevents player from pressing during enemy turns
        healButton.interactable = false;
        damageButton.interactable = false;
        blockButton.interactable = false;

        //heals player by 20
        _player.Heal(20);

        //switches game state to enemy's turn
        gameState = GameStates.enemy;
        NextGameState();
    }

    public void OnAttack()
    {
        healButton.interactable = false;
        damageButton.interactable = false;
        blockButton.interactable = false;

        //checks if enemy is blocking
        if (_enemy.isBlocking)
        {
            //if true, player deals 1 damage
            _enemy.Damage(1);
        }
        else
        {
            //if not blocking, deals 10 damage
            _enemy.Damage(10);
        }

        gameState = GameStates.enemy;
        NextGameState();
    }

    public void OnBlock()
    {
        healButton.interactable = false;
        damageButton.interactable = false;
        blockButton.interactable = false;

        //turns bool to true
        _player.isBlocking = true;

        gameState = GameStates.enemy;
        NextGameState();
    }
    #endregion

    #region Game States
    public IEnumerator PlayerTurn()
    {
        //checks if it is player's turn
        if (gameState == GameStates.player)
        {
            //sets blocking to false, used for future enemy turns
            _player.isBlocking = false;

            //lets player know it's their turn
            gameStatesText.text = "Game States: Your Turn!";
            //turns text color to red then back to white, to let player know what text has changed
            TextColor(gameStatesText, Color.red);

            yield return new WaitForSeconds(1f);

            TextColor(gameStatesText, Color.white);

            //enables button interactability
            healButton.interactable = true;
            damageButton.interactable = true;
            blockButton.interactable = true;
        }
    }

    public IEnumerator EnemyTurn()
    {
        //checks if it is enemy's turn
        if (gameState == GameStates.enemy)
        {
            _enemy.isBlocking = false;

            gameStatesText.text = "Game States: Enemy's Turn!";
            TextColor(gameStatesText, Color.red);

            yield return new WaitForSeconds(1f);

            TextColor(gameStatesText, Color.white);

            //calls for what action to take according to enemy health
            DecideAction();
        }
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
    public void DecideAction()
    {
        //i don't know how to make the enemy choose to block or not, so it's random
        int blockOrNot = Random.Range(0, 1);

        //1 = block, 0 = not
        if (blockOrNot == 1)
        {
            //switches enemy state to blocking
            enemyState = EnemyStates.shield;
            NextEnemyState();
        }
        else
        {
            //checks if health is low
            if (_enemy.isHealthLow())
            {
                //if true, enemy prioritises healing
                enemyState = EnemyStates.heal;
                NextEnemyState();
            }
            else
            {
                //if false, enemy prioritises attacking
                enemyState = EnemyStates.attack;
                NextEnemyState();
            }
        }
    }

    public IEnumerator Attack()
    {
        enemyStatesText.text = "Enemy States: Attacking!";
        TextColor(enemyStatesText, Color.red);

        yield return new WaitForSeconds(1f);

        TextColor(enemyStatesText, Color.white);

        //checks if player is blocking
        if (_player.isBlocking)
        {
            //if true, enemy deals 1 damage
            _player.Damage(1);
        }
        else
        {
            //if false, enemy deals 10 damage
            _player.Damage(10);
        }

        //lets player know they took damage
        TextColor(_player.healthText, Color.red);

        yield return new WaitForSeconds(1f);

        TextColor(_player.healthText, Color.white);

        //swtiches to player's turn
        gameState = GameStates.player;
        enemyState = EnemyStates.neutral;

        NextEnemyState();
        NextGameState();
    }

    public IEnumerator Healing()
    {
        enemyStatesText.text = "Enemy States: Healing!";
        TextColor(enemyStatesText, Color.red);

        yield return new WaitForSeconds(1f);

        TextColor(enemyStatesText, Color.white);

        _enemy.Heal(20);
        TextColor(_enemy.healthText, Color.green);

        yield return new WaitForSeconds(1f);

        TextColor(_enemy.healthText, Color.white);

        gameState = GameStates.player;
        enemyState = EnemyStates.neutral;

        NextEnemyState();
        NextGameState();
    }

    public IEnumerator Blocking()
    {
        enemyStatesText.text = "Enemy States: Blocking!";
        TextColor(enemyStatesText, Color.red);

        yield return new WaitForSeconds(1f);

        TextColor(enemyStatesText, Color.white);

        _enemy.isBlocking = true;

        gameState = GameStates.player;
        enemyState = EnemyStates.neutral;

        NextEnemyState();
        NextGameState();
    }
    #endregion

    public void TextColor(Text text, Color color)
    {
        text.color = (Color)color;
    }

    public void EndGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
