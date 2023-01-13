//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    GameCharacter currentTurnOwner;
    UIManager gameUIManager;
    ActionPointCounter actionPoints;
    public int CharacterPoints { get; private set; }

    public ActionPointCounter ActionPoints { get => actionPoints; private set => actionPoints = value; }
    public GameCharacter CurrentTurnOwner { get => currentTurnOwner; private set => currentTurnOwner = value; }

    public event System.Action onTurnFinished;


    private void Start()
    {
        gameUIManager = Game.Instance.UiManager;
        InitTurn(Game.Instance.Player);
        
        gameUIManager.ShowActionPointCount(actionPoints.ActionPointCount);
        //turnManager.onBeginNewTurn += InitTurn;
    }

    public void InitTurn(GameCharacter turnOwner)
    {
        CurrentTurnOwner = turnOwner;
        gameUIManager.ShowGameMessage($"Ход {CurrentTurnOwner}");
        actionPoints = new ActionPointCounter(UnityEngine.Random.Range(1, 6));
        //CharacterPoints =  actionPoints.ActionPointCount;
    }

    bool AllowAction(GameCharacter character)
    {
        if (character == CurrentTurnOwner)
        {
            return true;
        }
        return false;
    }

    public void PerformAnAction(GameCharacter character, GameCharacter target, Ability ability)
    {
        if (AllowAction(CurrentTurnOwner))
        {
            if (character.Target == null)
            {
                if (character is Player player)
                {
                    player.NextTarget();
                }
                //character.ChangeTarget();
            }

            
          

            if (ability.RequiredNumberOfPoints <= actionPoints.ActionPointCount)
            {
                switch (ability.actionType)
                {
                    case ActionType.Attack:
                        AttackLogic(character, ability, target);
                        break;
                    case ActionType.Treatment:
                        TreatmentLogic(character, ability);
                        break;
                    case ActionType.ApplyingAnEffect:
                        ApplyingAnEffectLogic(ability);
                        break;
                    default:
                        break;
                }
                actionPoints.SpendActionPoints(ability.RequiredNumberOfPoints);
                gameUIManager.ShowActionPointCount(actionPoints.ActionPointCount);

                if (ActionPoints.ActionPointCount <= 0)
                {
                    FinishTurn();
                }
            }
        }
    }

    void AttackLogic(GameCharacter character, Ability ability, GameCharacter target)
    {
        StartCoroutine(AttackAnimation(character, target));
        target.GetDamage(CalculateDamage(ability.Force, character.Attack, target.Defence));
    }

    void TreatmentLogic(GameCharacter character, Ability ability)
    {
        character.GetDamage(-ability.Force);
        Debug.Log($"Therment! Add Health: {ability.Force}");
    }

    void ApplyingAnEffectLogic(Ability ability)
    {
        Debug.Log("Cast effect on target");
    }

    void FinishTurn()
    {
        onTurnFinished?.Invoke();
    }

    float CalculateDamage(float forceAbility, float characterAttack, float targetDefence)
    {
        float damage = 0;
        damage = forceAbility * characterAttack * Random.Range(1, 7) - targetDefence * Random.Range(1, 7);
        return damage;
    }

    IEnumerator AttackAnimation(GameCharacter character, GameCharacter target)
    {
        character.CharacterAnimator.SetInteger("animationState", 1);
        yield return new WaitForSeconds(1);
        character.CharacterAnimator.SetInteger("animationState", 0);
    }
}
