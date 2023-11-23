using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Enemy body;
    ActionManager actionManager;
    TurnManager turnManager;

    AbilityInfo therment;
    AbilityInfo strongAttack;
    AbilityInfo strengthening;
    void Start()
    {
        InitAbilities();
        actionManager = Game.Instance.ActionManager;
        turnManager = Game.Instance.TurnManager;
        turnManager.onBeginNewTurn += EnemyTurn;
    }

    private void InitAbilities()
    {
        foreach (AbilityInfo abilityInfo in body.AbilitiesInfo)
        {
            if (abilityInfo.Ability.name.Contains("Therment"))
            {
                therment = abilityInfo;
            }
            else if (abilityInfo.Ability.actionType == ActionType.Attack && abilityInfo.Ability.Force >= 3)
            {
                strongAttack = abilityInfo;
            }
            else if (abilityInfo.Ability.actionType == ActionType.CastSpell && abilityInfo.Ability.name.Contains("Streng"))
            {
                strengthening = abilityInfo;
            }

        }
    }

    void DoAction()
    {
        if (body.AbilityPoints > 0)
        {
            if (therment != null&& therment.StateAbility && body.Health < 25)
            {
                body.ApplyAbility(therment.GetAbility());
                Debug.Log("therment");
                return;
            }
            else if (strengthening != null && strengthening.StateAbility && (body.TotalAttack - body.Target.TotalDefence < -2))
            {
                body.ApplyAbility(strengthening.GetAbility());
                Debug.Log("strengthening");
                return;
            }
            else if (strongAttack != null && strongAttack.StateAbility && body.AbilityPoints >= strongAttack.Ability.RequiredAbilityPoints)
            {
                body.ApplyAbility(strongAttack.GetAbility());
                Debug.Log("Strong!");
                return;
            }
        }
        body.ApplyAbility(body.BaseAbilityInfo.GetAbility());
    }

    void EnemyTurn(GameCharacter turnOwner)
    {
        if (turnOwner == body)
        {
            StartCoroutine(EnemyTurnCoroutine());
        }
    }

    IEnumerator EnemyTurnCoroutine()
    {
        while (actionManager.ActionPoints > 0)
        {
            if (body.Target == null)
            {
                break;
            }

            if (actionManager.CanDoAction)
            {
                yield return new WaitForSeconds(1);
                DoAction();
            }
            yield return null;
        }
    }
}
