using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buff
{
    [SerializeField] float buffAttack;
    [SerializeField] float buffDefence;
    [SerializeField] float buffHealth;
    [SerializeField] float buffMaxHealth;
    [SerializeField] float buffSpeed;
    [SerializeField] int duration;

    public Buff(float buffAttack = 0, float buffDefence = 0, float buffHealth = 0, float buffMaxHealth = 0, float buffSpeed = 0, int duration = 1)
    {
        this.buffAttack = buffAttack;
        this.buffDefence = buffDefence;
        this.buffHealth = buffHealth;
        this.buffMaxHealth = buffMaxHealth;
        this.buffSpeed = buffSpeed;
        this.duration = duration;
    }

    public int Duration { get => duration;}
    public float BuffAttack { get => buffAttack;}
    public float BuffDefence { get => buffDefence; }
    public float BuffHealth { get => buffHealth; }
    public float BuffSpeed { get => buffSpeed; }
    public float BuffMaxHealth { get => buffMaxHealth; }

    public void DecreaseDuration()
    {
        duration--;
    }
}
