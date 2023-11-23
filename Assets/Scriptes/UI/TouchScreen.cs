using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : AbilityButton, IPointerClickHandler
{
    //[SerializeField] Ability baseAbility;
    //Player player;

    //public Ability BaseAbility { get => baseAbility; private set => baseAbility = value; }

    //public event Action<GameCharacter> onBattleClick;
    bool active = true;

    void Start()
    {
        player = Game.Instance.Player;
        ApplyBaseAbility(player, WeaponType.None);
        player.onMainWeaponUpdated += ApplyBaseAbility;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (active)
        {
            OnCkickAbility();
        }
        
    }

    void ApplyBaseAbility(GameCharacter character, WeaponType weaponType)
    {
        AbilityInfo = player.BaseAbilityInfo;
    }

    public override void SetEnableButton(bool enable)
    {
        active = enable;
    }
}
