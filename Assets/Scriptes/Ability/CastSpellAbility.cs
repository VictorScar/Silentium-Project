using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Abilities/CastSpellAbility")]
public class CastSpellAbility : Ability
{
    //[SerializeReference, SubclassSelector] Spell spell;

    [SerializeField] float buffAttack = 0;
    [SerializeField] float buffDefence = 0;
    [SerializeField] float buffHealth = 0;
    [SerializeField] float buffMaxHealth = 0;
    [SerializeField] float buffSpeed = 0;
    [SerializeField] int duration = 1;
    public override void RunAbility(GameCharacter target)
    {
        Buff buff = new Buff(buffAttack, buffDefence, buffHealth, buffMaxHealth, buffSpeed, duration);
        target.AplyBuff(buff);
    }
}
