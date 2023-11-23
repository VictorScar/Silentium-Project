using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Strengthening : Spell
{
    //[SerializeReference, SubclassSelector] Buf buf;
    public override void SpellLogic(GameCharacter target, float force, int duration = 0)
    {
        base.SpellLogic(target, force, duration);

        //target.AplyBuff(buf);
    }
}
