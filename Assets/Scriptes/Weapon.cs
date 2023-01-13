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
    [SerializeField] GameObject model;
}
