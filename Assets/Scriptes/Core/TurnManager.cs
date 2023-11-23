using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    [SerializeField] List<GameCharacter> entrants = new List<GameCharacter>();

    GameCharacter turnOwner;
    ActionManager actionManager;
    [SerializeField] AnimationManager animationManager;

    int turnNumber = 0;

    public GameCharacter TurnOwner { get => turnOwner; private set => turnOwner = value; }
    public List<GameCharacter> Entrants { get => entrants; private set => entrants = value; }

    public event Action<GameCharacter> onBeginNewTurn;
    public event Action onBeginNewRound;

    private void Awake()
    {
        actionManager = Game.Instance.ActionManager;
    }

    private void Start()
    {
        //SetTurnOwner();
        animationManager.onEntryEnded += SetTurnOwner;
        actionManager.onTurnFinished += NextGameTurn;
    }

    public void RegisterEntrants(GameCharacter character)
    {
        Entrants.Add(character);
        character.onCharacterDie += RemoveEntrantce;
        onBeginNewRound += character.UpdateCharacterState;
    }

    public void RemoveEntrantce(GameCharacter character)
    {
        Entrants.Remove(character);
    }

    void NextGameTurn()
    {
        turnNumber++;
        if (turnNumber == Entrants.Count)
        {
            turnNumber = 0;
            onBeginNewRound?.Invoke();
        }

        SetTurnOwner();
    }

    private void SetTurnOwner()
    {
        CalculateQuee();

        if (turnNumber < Entrants.Count)
        {
            turnOwner = Entrants[turnNumber];
        }

        animationManager.onEntryEnded -= SetTurnOwner;
        StartCoroutine(NextGameTurnCoroutine());
    }

    IEnumerator NextGameTurnCoroutine()
    {
        yield return new WaitForSeconds(1);
        actionManager.InitTurn(turnOwner);
        onBeginNewTurn?.Invoke(turnOwner);
    }

    void CalculateQuee()
    {
        var d = Entrants.OrderByDescending(p => p.TotalSpeed).ToList();
        Entrants = d;
    }
}
