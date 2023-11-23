using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
    //[SerializeField] TMP_Text playerHealth;
    //[SerializeField] TMP_Text gameMessage;
    //[SerializeField] TMP_Text enemy2Health;
    //[SerializeField] TMP_Text enemy3Health;
    [SerializeField] PlayerInfo playerHealthBar;
    [SerializeField] List<EnemyInfo> enemyInfoBars;
    //[SerializeField] TMP_Text actionPointsCountText;

    [SerializeField] float disableOffset = 150f;

    //TurnManager turnManager;
    Player player;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {

    }

    public void Init(List<GameCharacter> entrants)
    {
        if (entrants.Count > 0)
        {
            foreach (GameCharacter character in entrants)
            {
                if (character is Player player)
                {
                    this.player = player;
                }
                else
                {
                    enemies.Add(character as Enemy);
                }
            }
            playerHealthBar.Init(player);
            InitEnemyBar(enemies);
            //UpdateFightUI();
        }

    }

    void InitEnemyBar(List<Enemy> enemies)
    {
        for (int i = 0; i < enemyInfoBars.Count; i++)
        {
            if (i < enemies.Count)
            {
                enemyInfoBars[i].Init(enemies[i]);
                enemyInfoBars[i].onCharacterClick += ChangeTargetHandle;
                enemyInfoBars[i].onInfoBarDeactive += RedrawEnemyBar;

                continue;
            }
            enemyInfoBars[i].DeactiveHealtBar(null);
            //RedrawEnemyBar(enemyInfoBars[i]);
        }
    }

    void RedrawEnemyBar(EnemyInfo enemyInfo)
    {
        int barIndex = enemyInfoBars.IndexOf(enemyInfo);

        if (barIndex < enemyInfoBars.Count - 1)
        {
            for (int i = barIndex; i < enemyInfoBars.Count; i++)
            {
                RectTransform barPosition = enemyInfoBars[i].GetComponent<RectTransform>();
                barPosition.localPosition = new Vector2(barPosition.localPosition.x, barPosition.localPosition.y + disableOffset);
            }
        }
    }

    //void UpdateFightUI()
    //{
    //    foreach (var enemyInfo in enemyInfoBars)
    //    {
    //        playerHealthBar.UpdateHealthText();
    //        enemyInfo.UpdateHealthText();
    //    }
    //}

    //void RemoveHealthBar(GameCharacter character)
    //{
    //    for (int i = 0; i < enemyInfoBars.Count; i++)
    //    {
    //        var enemyBar = enemyInfoBars[i];

    //        if (enemyBar.Enemy == character)
    //        {
    //            enemyBar.onCharacterClick -= ChangeTargetHandle;
    //            enemyBar.DeactiveHealtBar();
    //            RedrawEnemyBar(enemyBar);
    //        }

    //    }
    //}

    void ChangeTargetHandle(GameCharacter character)
    {
        player.ChangeTarget(character);
    }

}
