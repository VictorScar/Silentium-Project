using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Sprite Icon { get; set; }
    public AbilityInfo AbilityInfo { get => abilityInfo; set => abilityInfo = value; }

    [SerializeField] Button button;
    [SerializeField] protected AbilityInfo abilityInfo;
    protected Player player;
    RectTransform buttonPosition;
    Image iconUI;

    Vector2 disableOffset = new Vector2(200f, 0);

    bool enabledAbility = true;

    public event Action<bool> onClickAbility;
     

    public void InitAbilityButton(Player player, AbilityInfo abilityInfo)
    {
        this.player = player;
        this.abilityInfo = abilityInfo;
        iconUI = GetComponentInChildren<Image>();
        iconUI.sprite = abilityInfo.Ability.Icon;
        if (button != null)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnCkickAbility);
        }

        buttonPosition = GetComponent<RectTransform>();

        SetEnableButton(false);
    }

    public void OnCkickAbility()
    {
        onClickAbility?.Invoke(false);
        if (player.CanAction && abilityInfo.StateAbility)
        {
            Ability ability = abilityInfo.GetAbility();
            player.ApplyAbility(ability);
        }
        else if (!abilityInfo.StateAbility)
        {
            Debug.Log("Ability is not active");
        }

    }

    public virtual void SetEnableButton(bool enable)
    {
        button.enabled = enable;

        if (!enable)
        {
            if (enabledAbility)
            {
                buttonPosition.offsetMax += disableOffset;
                enabledAbility = false;
            }
            else
            {
                return;
            }

        }
        else
        {
            if (!enabledAbility)
            {
                buttonPosition.offsetMax -= disableOffset;
                enabledAbility = true;
            }

        }
    }
}
