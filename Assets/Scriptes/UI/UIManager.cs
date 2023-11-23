using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Color victoryColor;
    [SerializeField] Color defeatColor;
    [SerializeField] string victoryMessage;
    [SerializeField] string defeatMessage;

    [SerializeField] TMP_Text turnMessage;
    [SerializeField] TMP_Text gameMessage;
    [SerializeField] GameObject mainPanel;

    [SerializeField] FightUI fightUI;
    [SerializeField] ActionPointCountUI actionPointCountUI;
    [SerializeField] AbilityPointUI abilityPointUI;
    [SerializeField] AbilityButtonsManager buttonsManager;

    TurnManager turnManager;
    ActionManager actionManager;
    Player player;
    List<GameCharacter> entrants;

    private void Start()
    {
        player = Game.Instance.Player;
        turnManager = Game.Instance.TurnManager;
        actionManager = Game.Instance.ActionManager;

        fightUI.Init(turnManager.Entrants);
        actionPointCountUI.Init(actionManager);
        abilityPointUI.Init(player);

        turnManager.onBeginNewTurn += ShowGameMessage;
        player.onFightOver += ProcessGameEvent;
    }

    void ProcessGameEvent(bool isVictory)
    {
        if (isVictory)
        {
            ShowMainPanel(victoryMessage, victoryColor);
        }
        else
        {
            ShowMainPanel(defeatMessage, defeatColor);
        }
    }

    public void ShowGameMessage(GameCharacter character)
    {
        string message = $"’Ó‰ {character.CharacterName}";
        StartCoroutine(GameMessageCoroutine(message));
    }

    void ShowMainPanel(string message, Color textColor)
    {
        gameMessage.color = textColor;
        gameMessage.text = message;
        mainPanel.SetActive(true);
    }

    IEnumerator GameMessageCoroutine(string message)
    {
        turnMessage.gameObject.SetActive(true);
        turnMessage.text = message;
        yield return new WaitForSeconds(1);
        turnMessage.gameObject.SetActive(false);
    }
}
