using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public Sprite Icon { get; set; }
    public Ability Ability { get => ability; set => ability = value; }

    [SerializeField] Button button;
    [SerializeField] Ability ability;
    Player player;
    Image iconUI;
    public event Action <GameCharacter> onClickAbility;
    void Start()
    {
        //Icon = ability.Icon;
        player = Game.Instance.Player;
        iconUI = GetComponentInChildren<Image>();
        iconUI.sprite = ability.Icon;
        if (button!= null)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnCkickAbility);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCkickAbility()
    {
        onClickAbility?.Invoke(player);
        //Debug.Log("Click");
    }

    
}
