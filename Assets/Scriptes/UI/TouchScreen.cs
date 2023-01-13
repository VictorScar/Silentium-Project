using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreen : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Ability baseAbility;
    Player player;

    public Ability BaseAbility { get => baseAbility; private set => baseAbility = value; }

    public event Action <GameCharacter> onBattleClick;

    void Start()
    {
        player = Game.Instance.Player;
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onBattleClick?.Invoke(player);
        Debug.Log("BattleClick!");
    }
}
