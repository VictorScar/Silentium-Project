using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Abilities/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField] string abilityName;
    [SerializeField] int requiredNumberOfPoints = 0;
    [SerializeField] float force;
    [SerializeField] public ActionType actionType;
    [SerializeField] Sprite icon;
    [SerializeField] float RecoveryTime;
    [SerializeField] ParticleSystem effect;
    ActionManager actionManager;

    public int RequiredNumberOfPoints { get => requiredNumberOfPoints; private set => requiredNumberOfPoints = value; }
    public float Force { get => force; private set => force = value; }
    public Sprite Icon { get => icon; }

    private void Awake()
    {

    }

    public void Run(GameCharacter character)
    {
        if (actionManager == null)
        {
            actionManager = Game.Instance.ActionManager;
        }

        Debug.Log(abilityName);
        actionManager.PerformAnAction(character, character.Target, this);
    }
}
