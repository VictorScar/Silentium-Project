using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(menuName = "ScriptableObjects/Abilities/Ability")]
public abstract class Ability : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected int requiredNumberOfPoints = 0;
    [SerializeField] protected int requiredAbilityPoints = 0;
    [SerializeField] protected int prizeAbilityPoints = 0;
    [SerializeField] protected float force;
    [SerializeField] public ActionType actionType;
    [SerializeField] public AnimationType abilityAnimation;

    [SerializeField] Sprite icon;
    [SerializeField] GameObject effect;
    [SerializeField] AudioClip audio;

    [SerializeField] private int recoveryTime;

    public int RequiredNumberOfPoints { get => requiredNumberOfPoints; private set => requiredNumberOfPoints = value; }
    public float Force { get => force; private set => force = value; }
    public Sprite Icon { get => icon; }
    public int RequiredAbilityPoints { get => requiredAbilityPoints; }
    public int PrizeAbilityPoints { get => prizeAbilityPoints; }
    public GameObject Effect { get => effect; }
    public int RecoveryTime { get => recoveryTime; }
    public AudioClip Audio { get => audio; }

    //public bool IsCharacterMoving { get => isCharacterMoving; }

    //[SerializeField] protected bool isCharacterMoving = false;

    public virtual void RunAbility(GameCharacter target)
    {
       
    }
}
