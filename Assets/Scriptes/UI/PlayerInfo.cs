using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Slider playerHealthBar;
    [SerializeField] TMP_Text playerHealth;

    Player player;

    public void Init(Player player)
    {
        
        this.player = player;
        
        player.onHealthChanged += UpdateHealthText;
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        if (player != null)
        {
            playerHealth.text = $"{player.Health} / {player.MaxHealth}";
            playerHealthBar.value = player.Health / player.MaxHealth;
        }
        
    }
}
