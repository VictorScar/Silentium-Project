using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimation : StateMachineBehaviour
{
    ActionManager actionManager;
    TurnManager turnManager;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (actionManager == null)
        {
            actionManager = Game.Instance.ActionManager;
        }

        if (turnManager == null)
        {
            turnManager = Game.Instance.TurnManager;
            foreach (GameCharacter entrant in turnManager.Entrants)
            {
                entrant.onCharacterDie += CharacterDeathAnimation;
            }
        }

    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (animator == turnManager.TurnOwner.CharacterAnimator)
    //    {
    //        switch (actionManager.CharacterState)
    //        {
    //            case CharacterState.None:
    //                animator.SetBool("isMoving", false);
    //                animator.SetBool("isBlocking", false);
    //                break;
    //            case CharacterState.Move:
    //                animator.SetBool("isMoving", true);
    //                break;
    //            case CharacterState.Attack:
    //                animator.SetTrigger("isAttack");
    //                break;
    //            case CharacterState.Blocking:
    //                animator.SetBool("isBlocking", true);
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    public void RunAnimationState(GameCharacter character, AnimationType animationType, bool enable = true, int entryType=0)
    {
        switch (animationType)
        {
            case AnimationType.None:
                break;
            case AnimationType.SimpleAttack:
                StartAttackAnimation(character, 0);
                break;
            case AnimationType.StrongAttack:
                StartAttackAnimation(character, 2);
                break;
            case AnimationType.Shooting:
                StartAttackAnimation(character, 4);
                break;
            case AnimationType.Blocking:
                StartDamageAnimation(character, true);
                break;
            case AnimationType.Wounded:
                StartDamageAnimation(character, false);
                break;
            case AnimationType.CastSpell:
                CastSpellAnimation(character);
                break;
            case AnimationType.Movement:
                StartMovementAnimation(character, enable);
                break;
            case AnimationType.Entry:
                CharacterEntryAnimation(character, entryType);
                break;
            case AnimationType.Combined:
                break;
            default:
                break;
        }
    }
    void StartMovementAnimation(GameCharacter character, bool enable)
    {
        if (enable)
        {
            character.CharacterAnimator.SetFloat("speed", character.MoveSpeed);
        }
        else
        {
            character.CharacterAnimator.SetFloat("speed", 0);
        }
    }

    void StartAttackAnimation(GameCharacter character, int attackType)
    {
        character.CharacterAnimator.SetInteger("attackType", attackType);
        character.CharacterAnimator.SetTrigger("isAttack");
    }

    void StartDamageAnimation(GameCharacter character, bool isBlocking)
    {
        if (isBlocking)
        {
            character.CharacterAnimator.SetFloat("defence", 1f);
        }
        else
        {
            character.CharacterAnimator.SetFloat("defence", 0f);
        }

        character.CharacterAnimator.SetTrigger("isBlocking");
    }

    void CastSpellAnimation(GameCharacter character)
    {
        character.CharacterAnimator.SetTrigger("isCastSpell");
    }

    public void SetWeaponTypeAnimation(GameCharacter character, WeaponType weaponType)
    {
        if (weaponType == WeaponType.OneHand)
        {
            character.CharacterAnimator.SetFloat("WeaponType", 0);
        }
        else
        {
            character.CharacterAnimator.SetFloat("WeaponType", 1);
        }
    }

    void CharacterDeathAnimation(GameCharacter character)
    {
        character.CharacterAnimator.SetTrigger("isDead");
    }

    void CharacterEntryAnimation(GameCharacter character, int entryType)
    {
        character.CharacterAnimator.SetInteger("entryType", entryType);
        character.CharacterAnimator.SetTrigger("isEntryCharacter");
    }
    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
