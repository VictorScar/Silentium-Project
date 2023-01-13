using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameCharacter
{
    PlayerStorage playerStorage;
    int targetNumber = 0;
    List<Enemy> enemies;
    protected override void Awake()
    {
        base.Awake();
        Game.Instance.Player = this;
        CharacterName = "Player";
    }

    private void Start()
    {
        enemies = Game.Instance.Enemies;
        Target = enemies[targetNumber];
    }

    public override void ChangeTarget(GameCharacter enemy)
    {
        base.ChangeTarget(enemy);
              
    }

    public void NextTarget()
    {
        if (targetNumber < enemies.Count)
        {
            targetNumber++;
        }
    }
}
