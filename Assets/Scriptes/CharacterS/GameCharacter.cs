using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameCharacter : MonoBehaviour
{
    [Header("Base parameters")]
    [SerializeField] float health;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float baseAttack = 2;
    [SerializeField] float baseDefence = 2;
    [SerializeField] float baseSpeed = 2;

    //Modified parameters
    float modifiedMaxHealth = 0;
    float modifiedAttack = 0;
    float modifiedDefence = 0;
    float modifiedSpeed = 0;

    [SerializeField] float totalDefence;
    [SerializeField] float totalAttack;
    [SerializeField] float totalSpeed;


    [SerializeField] float moveSpeed = 2;

    //[SerializeField] int level=1;
    protected bool canAction = true;

    [SerializeField] protected bool hasEntryAnimation = false;

    //—сылки на требуемые компоненты
    [SerializeField] CharacterInventory inventory;

    protected ActionManager actionManager;
    [SerializeField] protected Animator characterAnimator;
    [SerializeField] protected string characterName;
    [SerializeField] protected int abilityPoints = 0;

    //—пособности
    [SerializeField] Ability baseAbility;
    [SerializeField] AbilityInfo baseAbilityInfo;
    [SerializeField] List<AbilityInfo> abilitiesInfo;
    //[SerializeField] List<Ability> abilities;
    [SerializeField] List<Buff> activeBuffs = new List<Buff>();

    protected TurnManager turnManager;

    GameCharacter target = null;

    public event Action onHealthChanged;
    public event Action<GameCharacter> onCharacterDie;
    public event Action<GameCharacter, WeaponType> onMainWeaponUpdated;
    // public event Action<GameCharacter> onAllEnemiesDestryed;

    public float Health { get => health; set => health = value; }

    // public List<Ability> Abilities { get => abilities; private set => abilities = value; }
    public float MaxHealth { get => maxHealth; }
    public GameCharacter Target { get => target; set => target = value; }
    public Animator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }

    public string CharacterName { get => characterName; protected set => characterName = value; }
    public float MoveSpeed { get => moveSpeed; protected set => moveSpeed = value; }
    public bool CanAction { get => canAction; }
    public int AbilityPoints { get => abilityPoints; }
    public float TotalAttack { get => totalAttack; }
    public float TotalDefence { get => totalDefence; }
    public Ability BaseAbility { get => baseAbility; }
    public float TotalSpeed { get => totalSpeed; }
    public List<AbilityInfo> AbilitiesInfo { get => abilitiesInfo; }
    public AbilityInfo BaseAbilityInfo { get => baseAbilityInfo; }

    protected virtual void Awake()
    {
        turnManager = Game.Instance.TurnManager;
        actionManager = Game.Instance.ActionManager;
        turnManager.RegisterEntrants(this);
        inventory.onInventoryUpdated += UpdateUseWeapon;
        totalSpeed = baseSpeed;
    }

    void UpdateUseWeapon()
    {
        baseAbilityInfo = inventory.MainWeaponSlot.WeaponAbility;
        onMainWeaponUpdated?.Invoke(this, inventory.MainWeaponSlot.WeaponType);
        UpdateStats();
    }

    private void UpdateStats()
    {
        totalAttack = baseAttack + inventory.MainWeaponSlot.Attack + modifiedAttack;
        totalDefence = baseDefence + inventory.MainWeaponSlot.Defense + modifiedDefence;
        totalSpeed = baseSpeed + modifiedSpeed;
        maxHealth += modifiedMaxHealth;
    }

    public void ApplyAbility(Ability ability)
    {
        if (actionManager.AllowAction(this) && ability != null)
        {
            actionManager.InitAction(this, ability);
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        onHealthChanged?.Invoke();

        if (health <= 0)
        {
            CharacterDeath();
        }
    }

    public virtual void ChangeTarget(GameCharacter enemy)
    {
        Target = enemy;
        transform.LookAt(Target.transform.position, transform.up);
        Target.onCharacterDie += RemoveTarget;
    }

    public virtual void NextTarget()
    {

    }

    public void AplyBuff(Buff buff)
    {
        activeBuffs.Add(buff);
        ProcessBuffs();
    }

    public virtual void UpdateCharacterState()
    {
        UpdateBuffCounter();
        UpdateAbilitiesRecoveryTime();
    }

    void ProcessBuffs()
    {
        modifiedAttack = 0;
        modifiedDefence = 0;
        modifiedSpeed = 0;
        modifiedMaxHealth = 0;

        foreach (Buff buff in activeBuffs)
        {
            modifiedAttack += buff.BuffAttack;
            modifiedDefence += buff.BuffDefence;
            modifiedSpeed += buff.BuffSpeed;
            modifiedMaxHealth += buff.BuffMaxHealth;

            if (health + buff.BuffHealth <= maxHealth)
            {
                GetDamage(-buff.BuffHealth);
            }
            else
            {
                health = maxHealth;
            }

        }

        UpdateStats();
    }

    public void UpdateBuffCounter()
    {
        for (int i = 0; i < activeBuffs.Count; i++)
        {
            Buff buff = activeBuffs[i];
            buff.DecreaseDuration();

            if (buff.Duration <= 0)
            {
                activeBuffs.Remove(buff);
            }
        }
        ProcessBuffs();
    }

    void UpdateAbilitiesRecoveryTime()
    {
        foreach (AbilityInfo abilityInfo in abilitiesInfo)
        {
            abilityInfo.DecreaseRecoveruTime();
        }
    }

    public virtual void ChangeAbilityPointsCount(int points)
    {
        abilityPoints += points;
    }

    protected void RemoveTarget(GameCharacter character)
    {
        if (Target == character)
        {
            Target = null;
            NextTarget();
        }
    }

    public virtual void CharacterDeath()
    {
        onCharacterDie?.Invoke(this);
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
