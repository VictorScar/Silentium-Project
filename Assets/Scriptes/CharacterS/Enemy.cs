using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : GameCharacter
{
    [SerializeField] EnemyAI aI;
    protected override void Awake()
    {
        base.Awake();
        Game.Instance.EnemiesPrefab.Add(this);
    }

    void Start()
    {
        NextTarget();
    }

    public override void NextTarget()
    {
        foreach (var character in turnManager.Entrants)
        {
            if (character is Player)
            {
                ChangeTarget(character);
                return;
            }
        }
        Target = null;
        base.NextTarget();
    }

    public override void CharacterDeath()
    {
        base.CharacterDeath();
    }
}
