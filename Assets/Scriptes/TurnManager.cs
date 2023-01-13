using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] List<GameCharacter> entrants = new List<GameCharacter>();
    [SerializeField] UIManager uiManager;
    GameCharacter turnOwner;
    ActionManager actionManager;
    int turnNumber = 0;
    bool isInit = true;
   
    public GameCharacter TurnOwner { get => turnOwner; private set => turnOwner = value; }
    public List<GameCharacter> Entrants { get => entrants; private set => entrants = value; }

    //public event Action <GameCharacter> onBeginNewTurn;

    private void Awake()
    {
        actionManager = Game.Instance.ActionManager;
    }

    private void Start()
    {
        //turnOwner = Entrants[i];
        NextGameTurn();
        isInit = false;
        actionManager.onTurnFinished += NextGameTurn;
        uiManager.ShowGameMessage($"’Ó‰ {turnOwner.CharacterName}");
        
    }

    public void RegisterEntrants(GameCharacter character)
    {
        Entrants.Add(character);
    }

    void NextGameTurn()
    {
        if (!isInit)
        {
            turnNumber++;
        }
        if (turnNumber == Entrants.Count)
        {
            turnNumber = 0;
        }

        turnOwner = Entrants[turnNumber];
        actionManager.InitTurn(turnOwner);
        uiManager.ShowGameMessage($"’Ó‰ {turnOwner.CharacterName}");

        if (turnOwner is Enemy)
        {
            Enemy enemy = turnOwner as Enemy;
            enemy.NPCAttack();
        }
    }

    //void CalculateQuee ()
    //{
    //    int[]
    //    foreach (GameCharacter character in entrants)
    //    {

    //    }
    //    Mathf.Max(entrants)
    //}
}
