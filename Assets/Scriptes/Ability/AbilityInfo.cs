using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityInfo
{
    [SerializeField] Ability ability;

    [SerializeField] int recoveryTime = 0;
    [SerializeField] bool stateAbility = true;

    public Ability Ability { get => ability; private set => ability = value; }
    public int RecoveryTime { get => recoveryTime; }
    public bool StateAbility { get => stateAbility; }

    void CheckAbilityState()
    {
        if (recoveryTime == 0)
        {
            stateAbility = true;
        }
        else
        {
            stateAbility = false;
        }
    }

    public Ability GetAbility()
    {
        
        if (stateAbility)
        {
            recoveryTime = ability.RecoveryTime;
            CheckAbilityState();
            return ability;
        }
        return null;
    }

    public void DecreaseRecoveruTime()
    {
        if (recoveryTime > 0)
        {
            recoveryTime--;
        }
        CheckAbilityState();

    }
}
