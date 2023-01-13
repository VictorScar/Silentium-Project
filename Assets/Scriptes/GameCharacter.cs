using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameCharacter : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float maxHealth = 100;
    [SerializeField] float attack = 2;
    [SerializeField] float defence = 2;
    [SerializeField] float speed = 2;
    //[SerializeField] int level=1;

    [SerializeField] CharacterInventory inventory;
    [SerializeField] List<Ability> abilities;
    protected ActionManager actionManager;
    [SerializeField] protected Animator characterAnimator;

    TurnManager turnManager;

    GameCharacter target = null;

    public event Action onHealthChanged;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Defence { get => defence; set => defence = value; }
    public List<Ability> Abilities { get => abilities; private set => abilities = value; }
    public float MaxHealth { get => maxHealth; }
    public GameCharacter Target { get => target; set => target = value; }
    public Animator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }
    public float Attack { get => attack; private set => attack = value; }
    public string CharacterName { get; protected set; }

    protected virtual void Awake()
    {
        turnManager = Game.Instance.TurnManager;
        actionManager = Game.Instance.ActionManager;
        turnManager.RegisterEntrants(this);
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

    public virtual void ChangeTarget(GameCharacter gameCharacter)
    {

    }

    void CharacterDeath()
    {
        Destroy(gameObject);
    }
}
