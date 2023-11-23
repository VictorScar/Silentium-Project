using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;
//TODO отделить логику действий в отдельный класс
public class ActionManager : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;


    int actionPoints = 0;

    [SerializeField] Vector3 offsetToTargetInAttackState;//TODO is animationManager
    [SerializeField] float attackDistance = 1;
    bool canDoAction = true;
    bool isAttackEnded = false;


    public int ActionPoints { get => actionPoints; private set => actionPoints = value; }
    public bool CanDoAction { get => canDoAction; }

    public event Action onTurnFinished;
    public event Action<int> onActionPointUpdated;
    public event Action<GameCharacter, bool, IEnumerator> onCharacterMovement;
    public event Action<GameCharacter, GameCharacter, float, Ability> onCharacterBeginAttack;//TODO использовть делегат с осмысленными параметрами
    // TODO использовать Struct для передачи параметров в событие или класс
    public event Action<GameCharacter, bool> onCharacterBlocking;
    public event Action<GameCharacter, Ability> onCharacterBeginCastSpell;

    private void Start()
    {

    }

    public void InitTurn(GameCharacter turnOwner)
    {
        actionPoints = ((int)turnManager.TurnOwner.TotalSpeed);
        onActionPointUpdated?.Invoke(actionPoints);
    }

    public void InitAction(GameCharacter character, Ability ability)
    {
        if (!canDoAction)
        {
            return;
        }

        if (character.Target == null) //TODO Убрать
        {
            return;
        }

        if (actionPoints >= ability.RequiredNumberOfPoints && character.AbilityPoints >= ability.RequiredAbilityPoints) //TODO refact
        {
            canDoAction = false;
            switch (ability.actionType) // Обдумать применение обычного класса вместо свича
            {
                case ActionType.Attack:
                    StartCoroutine(AttackCoroutine(character, character.Target, ability, true)); break;
                case ActionType.Shoot:
                    StartCoroutine(AttackCoroutine(character, character.Target, ability, false)); break;
                case ActionType.CastSpell:
                    StartCoroutine(CastBuffCoroutine(character, character.Target, ability)); break;
                case ActionType.Combined:
                    break;
                default:
                    break;
            }
        }
    }

    public bool AllowAction(GameCharacter character) => character == turnManager.TurnOwner;

    Vector3 DetermineDirection(GameCharacter character, GameCharacter target)//TODO добавить точку атаки персонажам
    {
        return target.transform.position + target.transform.forward * attackDistance;
    }

    public void SetAttackEnded()
    {
        isAttackEnded = true;
    }

    float CalculateDamage(float forceAbility, float characterAttack, float targetDefence)
    {

        float summaryAttack = (characterAttack + forceAbility) * Random.Range(1, 7);
        float summaryDefence = targetDefence * Random.Range(1, 7);

        return Mathf.Max(summaryAttack - summaryDefence, 0);
    }

    public void PerformAnAction(GameCharacter character, Ability ability)
    {
        actionPoints -= (ability.RequiredNumberOfPoints);
        character.ChangeAbilityPointsCount(ability.PrizeAbilityPoints);
        character.ChangeAbilityPointsCount(-ability.RequiredAbilityPoints);
        
        onActionPointUpdated?.Invoke(actionPoints);

        if (character.Target == null)
        {
            character.NextTarget();
        }
        canDoAction = true;

        if (actionPoints == 0)
        {
            onTurnFinished?.Invoke();
        }
    }

    public IEnumerator CharacterMove(GameCharacter character, Vector3 targetPosition) //TODO DoTween
    {
        character.transform.LookAt(targetPosition);

        while (character.transform.position != targetPosition)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position,
                                                        targetPosition, character.MoveSpeed * Time.deltaTime);
            yield return null;
        }
        if (character.Target != null)
        {
            character.transform.LookAt(character.Target.transform.position);
        }
    }

    public IEnumerator AttackCoroutine(GameCharacter character, GameCharacter target, Ability ability, bool shouldMove)
    {
        isAttackEnded = false;
        Vector3 defaultPosition = character.transform.position;
        Vector3 targetPosition = DetermineDirection(character, target);
        float damage = CalculateDamage(ability.Force, character.TotalAttack, target.TotalDefence);

        if (shouldMove)
        {
            IEnumerator moveCoroutine = CharacterMove(character, targetPosition);
            onCharacterMovement?.Invoke(character, true, moveCoroutine); //TODO из 2 мест запрашивают возврат корутины, а надо из 1ого
            yield return moveCoroutine;
        }

        onCharacterBeginAttack?.Invoke(character, target, damage, ability);

        yield return new WaitUntil(() => isAttackEnded);

        target.GetDamage(damage);

        if (shouldMove)
        {
            IEnumerator moveCoroutine = CharacterMove(character, defaultPosition);
            onCharacterMovement?.Invoke(character, false, moveCoroutine);
            yield return moveCoroutine;
        }
        PerformAnAction(character, ability);
    }

    IEnumerator CastBuffCoroutine(GameCharacter character, GameCharacter target, Ability ability)
    {
        onCharacterBeginCastSpell?.Invoke(character, ability);
        yield return new WaitForSeconds(1f);
        ability.RunAbility(character);
        PerformAnAction(character, ability);
    }
}
