using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    ActionManager actionManager;
    [SerializeField] AnimationManager animationManager;

    [SerializeField] GameObject damageEffect;
    [SerializeField] GameObject damageEffect2;
    [SerializeField] GameObject blockEffect;
    [SerializeField] GameObject treatmentEffect;
    [SerializeField] GameObject castEffect1;
    [SerializeField] GameObject castEffect2;

    void Start()
    {
        actionManager = Game.Instance.ActionManager;
        //actionManager.onCharacterBlocking += CastDamageEffect;
        actionManager.onCharacterBeginCastSpell += CastSpellEffect;
        animationManager = Game.Instance.AnimationManager;
        animationManager.onDamageTarget += CastDamageEffect;
        animationManager.onAttackEffect += CastAttackEffect;
    }

    private void CastAttackEffect(GameCharacter character, GameObject effect)
    {
        StartCoroutine(CastEffectCoroutine(character, effect, null, 1f));
    }

    public void CastDamageEffect(GameCharacter character, bool isGetDamage)
    {
        if (isGetDamage)
        {
            StartCoroutine(CastEffectCoroutine(character, damageEffect, damageEffect2, 1.5f));
        }
        else
        {
            StartCoroutine(CastEffectCoroutine(character, blockEffect, null, 1.5f));
        }

    }

    void CastSpellEffect(GameCharacter target, Ability ability)
    {
        StartCoroutine(CastEffectCoroutine(target, ability.Effect, null, 2));
    }

    IEnumerator CastEffectCoroutine(GameCharacter character, GameObject effect, GameObject effect2, float duration)
    {
        yield return new WaitForSeconds(0.5f);
        var effectInstance = Instantiate(effect, character.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(duration);
        Destroy(effectInstance);

        if (effect2 != null)
        {
            var effectInstance2 = Instantiate(effect2, character.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(duration - 0.5f);
            Destroy(effectInstance2);
        }
    }
}
