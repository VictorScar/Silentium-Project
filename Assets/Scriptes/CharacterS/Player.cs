using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : GameCharacter
{
    PlayerStorage playerStorage;
   
    //Player events
    public event Action<int> onAbilityPointCountChanged;
    public event Action<bool> onFightOver;

    protected override void Awake()
    {
        base.Awake();
        Game.Instance.Player = this;

    }

    private void Start()
    {
        NextTarget();
    }

    public override void NextTarget()
    {
        foreach (var enemy in turnManager.Entrants)
        {
            var nextTarget = enemy as Enemy;

            if (nextTarget != null)
            {
                ChangeTarget(nextTarget);
                return;
            }
        }
        Target = null;
        base.NextTarget();

        onFightOver?.Invoke(true);
    }

    public override void UpdateCharacterState()
    {
        base.UpdateCharacterState();
        onAbilityPointCountChanged?.Invoke(AbilityPoints);
    }

    public override void ChangeAbilityPointsCount(int points)
    {
        base.ChangeAbilityPointsCount(points);
        onAbilityPointCountChanged?.Invoke(AbilityPoints);
    }

    public override void CharacterDeath()
    {
        base.CharacterDeath();
        onFightOver?.Invoke(false);
    }
}
