using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameCharacter
{
    protected override void Awake()
    {
        base.Awake();
        Game.Instance.Enemies.Add(this);
        CharacterName = "Enemy";
    }

    void Start()
    {
        Target = Game.Instance.Player;
    }

    void Update()
    {

    }

    public void NPCAttack()
    {
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        while (actionManager.ActionPoints.ActionPointCount > 0)
        {
            yield return new WaitForSeconds(1);
            actionManager.PerformAnAction(this, Target, Abilities[0]);
        }
    }
}
