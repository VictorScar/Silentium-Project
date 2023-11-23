using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AnimationManager animationManager;
    [SerializeField] ActionManager actionManager;

    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip blockSound;
    [SerializeField] AudioClip spellSound;
    [SerializeField] AudioClip trainMusic;

    private void Start()
    {
        InitSoundManager();
    }
    void InitSoundManager()
    {
        animationManager.onDamageTarget += DamageSound;
        actionManager.onCharacterBeginCastSpell += SpellSound;
    }

    void DamageSound(GameCharacter character, bool isDamage)
    {
        if (isDamage)
        {
            if (damageSound != null)
            {
                StartCoroutine(PlaySoundCotoutine(damageSound, 0.5f));
                return;
            }
            Debug.Log("Нет звука урона");
        }
        else
        {
            if (blockSound != null)
            {
                StartCoroutine(PlaySoundCotoutine(blockSound, 0.2f));
                return;
            }
            Debug.Log("Нет звука блока");
        }
    }

    void SpellSound(GameCharacter character, Ability ability)
    {
        if (ability.Audio != null)
        {
            StartCoroutine(PlaySoundCotoutine(ability.Audio, 0));
        }
    }

    IEnumerator PlaySoundCotoutine(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = clip;
        audioSource.Play();
    }
}
