using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] SpawnEntrants spawner;
    [SerializeField] ActionManager actionManager;
    [SerializeField] TurnManager turnManager;
    [SerializeField] ControlAnimation controller;

    public event Action<GameCharacter> onAttackStarted;
    public event Action onAttackFinished;
    public event Action<GameCharacter, bool> onDamageTarget;
    public event Action<GameCharacter> onEntryStarted;
    public event Action onEntryEnded;
    public event Action<GameCharacter, GameObject> onAttackEffect;

    private void Start()
    {
        actionManager.onCharacterMovement += MovementAnimation;
        actionManager.onCharacterBeginAttack += AttackAnimation;
        actionManager.onCharacterBeginCastSpell += CastSpellAnimation;
        onAttackFinished += actionManager.SetAttackEnded;
        spawner.onCharacterSpawn += StartEntryAnimation;

    }

    public void InitControllAnimator(GameCharacter character)
    {
        controller = character.CharacterAnimator.GetBehaviour<ControlAnimation>();
    }

    public void MovementAnimation(GameCharacter character, bool enable, IEnumerator moveCoroutine)
    {
        StartCoroutine(MovementAnimationCoroitune(character, character.MoveSpeed, moveCoroutine));
    }

    public void AttackAnimation(GameCharacter character, GameCharacter target, float damage, Ability ability)
    {
        StartCoroutine(AttackAnimationCoroutine(character, target, damage, ability));
    }

    public void CastSpellAnimation(GameCharacter character, Ability ability)
    {
        StartCoroutine(CastSpellAnimationCoroutine(character));
    }

    public void StartEntryAnimation(GameCharacter character, Vector3 startPosition, int entryType)
    {
        ListenCharacterWeaponUpdated();
        StartCoroutine(EntryAnimationCoroutine(character, startPosition, entryType));
    }

    void SetAnimtionWeaponType(GameCharacter character, WeaponType weaponType)
    {
        controller.SetWeaponTypeAnimation(character, weaponType);
    }

    void ListenCharacterWeaponUpdated()
    {
        foreach (GameCharacter character in turnManager.Entrants)
        {
            character.onMainWeaponUpdated += SetAnimtionWeaponType;
        }
    }

    IEnumerator MovementAnimationCoroitune(GameCharacter character, float speed, IEnumerator moveCoroutine)
    {
        controller.RunAnimationState(character, AnimationType.Movement, true);
        onAttackStarted?.Invoke(character.Target);
        yield return moveCoroutine;//TODO
        controller.RunAnimationState(character, AnimationType.Movement, false);
    }

    IEnumerator AttackAnimationCoroutine(GameCharacter character, GameCharacter target, float damage, Ability ability)
    {
        controller.RunAnimationState(character, ability.abilityAnimation);

        if (ability.Effect!=null)
        {
            onAttackEffect?.Invoke(character, ability.Effect);
        }

        if (damage < 2)
        {
            yield return new WaitForSeconds(0.5f);
            controller.RunAnimationState(target, AnimationType.Blocking);
            onDamageTarget?.Invoke(target, false);
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            onDamageTarget?.Invoke(target, true);
            controller.RunAnimationState(target, AnimationType.Wounded);

        }


        while (!character.CharacterAnimator.IsInTransition(0))
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        onAttackFinished();
    }

    IEnumerator CastSpellAnimationCoroutine(GameCharacter character)
    {
        controller.RunAnimationState(character, AnimationType.CastSpell);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator EntryAnimationCoroutine(GameCharacter character, Vector3 startPosition, int entryType)
    {
        InitControllAnimator(character);
        
        if (character is Player)
        {
            controller.RunAnimationState(character, AnimationType.Entry, false, entryType);
            onEntryStarted?.Invoke(character);

            while (character.transform.position != startPosition)
            {
                character.transform.position = Vector3.MoveTowards(character.transform.position, startPosition, 1 * Time.deltaTime);
                yield return null;
            }

            onEntryEnded?.Invoke();
        }
        else
        {
            yield return new WaitForSeconds(5f);
            controller.RunAnimationState(character, AnimationType.Entry, false, entryType);
        }

    }
    private void OnDestroy()
    {
        actionManager.onCharacterMovement -= MovementAnimation;
        actionManager.onCharacterBeginAttack -= AttackAnimation;
        actionManager.onCharacterBeginCastSpell -= CastSpellAnimation;
        onAttackFinished -= actionManager.SetAttackEnded;
        spawner.onCharacterSpawn -= StartEntryAnimation;
    }
}
