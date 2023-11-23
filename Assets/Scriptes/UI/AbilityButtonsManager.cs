using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonsManager : MonoBehaviour
{
    [SerializeField] AbilityButton[] buttons;
    List <AbilityButton> activeButtons = new List<AbilityButton>();
    [SerializeField] TurnManager turnManager;
    [SerializeField] Player player;


    private void Start()
    {
        StartCoroutine(InitButtonManager());
    }

    void AplyChangeAbility(GameCharacter character, WeaponType weaponType)
    {
        IniteAbilitiesButton(player.AbilitiesInfo);
    }

    void IniteAbilitiesButton(List<AbilityInfo> playerAbilities)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            AbilityButton button = buttons[i];
            if (i < playerAbilities.Count)
            {
                AbilityInfo abilityInfo = playerAbilities[i];
                activeButtons.Add(button);
                button.InitAbilityButton(player, abilityInfo);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    void UpdateButtonsMode(int abilityPoints)
    {
        foreach (var button in activeButtons)
        {
            if (abilityPoints >= button.AbilityInfo.Ability.RequiredAbilityPoints && button.AbilityInfo.StateAbility)
            {
                button.SetEnableButton(true);
            }
            else
            {
                button.SetEnableButton(false);
            }
        }
    }

    IEnumerator InitButtonManager()
    {
        yield return new WaitUntil(() => Game.Instance.Player != null);
        player = Game.Instance.Player;
        //yield return new WaitForSeconds(5);
        IniteAbilitiesButton(player.AbilitiesInfo);
        player.onAbilityPointCountChanged += UpdateButtonsMode;
        player.onMainWeaponUpdated += AplyChangeAbility;
    }
}
