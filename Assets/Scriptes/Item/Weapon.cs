using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Weapon", fileName = "Weapon")]
public class Weapon : Item
{
    [SerializeField] string weaponName;
    [SerializeField] string description;
    [SerializeField] float attack;
    [SerializeField] float defense;
    [SerializeField] Sprite icon;
    [SerializeField] WeaponType weaponType = WeaponType.None;
    [SerializeField] AbilityInfo weaponAbility;

    public float Attack { get => attack; }
    public float Defense { get => defense; }
    public string WeaponName { get => weaponName; }
    public AbilityInfo WeaponAbility { get => weaponAbility; }
    public WeaponType WeaponType { get => weaponType; }
}
