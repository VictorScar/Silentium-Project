using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ThermentSpell : Spell
{
    public override void SpellLogic(GameCharacter target, float force, int duration = 0)
    {
        target.GetDamage(-force);
    }

}
