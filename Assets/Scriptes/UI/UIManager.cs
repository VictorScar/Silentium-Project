using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text playerHealth;
    [SerializeField] TMP_Text gameMessage;
    [SerializeField] TMP_Text enemy2Health;
    [SerializeField] TMP_Text enemy3Health;
    [SerializeField] Slider playerHealthBar;
    [SerializeField] EnemyInfo[] enemyInfoBars;
    [SerializeField] TMP_Text actionPointsCountText;


    [SerializeField] AbilityButton[] buttons;
    [SerializeField] TouchScreen touchScreen;
    
    Player player;
    List<Enemy> enemies;

    private void Start()
    {
        player = Game.Instance.Player;
        IniteAbilitiesButton(player.Abilities);
        enemies = Game.Instance.Enemies;
        player.onHealthChanged += UpdateFightUI;
        IniteFightUI();
        
        foreach (Enemy enemy in enemies)
        {
            enemy.onHealthChanged += UpdateFightUI;
        }
    }
    
    void IniteAbilitiesButton(List<Ability> abilitiesPlayer)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            AbilityButton button = buttons[i];
            if (i < abilitiesPlayer.Count)
            {
                Ability ability = abilitiesPlayer[i];
                button.Icon = ability.Icon;
                button.Ability = ability;
                button.onClickAbility += ability.Run;
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

        touchScreen.onBattleClick += touchScreen.BaseAbility.Run;
    }

    void IniteFightUI()
    {
        UpdateFightUI();
        int i = 0;
        foreach (EnemyInfo enemyBar in enemyInfoBars)
        {
            if (i < enemies.Count)
            {
                enemyBar.InitObject(enemies[i]);
                i++;
            }
            else
            {
                enemyBar.gameObject.SetActive(false);
            }
        }
    }

    void UpdateFightUI()
    {
        playerHealth.text = $"{player.Health} / {player.MaxHealth}";
        playerHealthBar.value = player.Health / player.MaxHealth;
    }

    public void ShowGameMessage(string message)
    {
        StartCoroutine(gameMessageCoroutine(message));
    }

    public void ShowActionPointCount(int pointsCount)
    {
        actionPointsCountText.text = pointsCount.ToString();
    }

    IEnumerator gameMessageCoroutine(string message)
    {
        gameMessage.gameObject.SetActive(true);
        gameMessage.text = message;
        yield return new WaitForSeconds(1);
        gameMessage.gameObject.SetActive(false);
    }
}
