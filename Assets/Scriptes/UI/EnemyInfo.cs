using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour, IPointerClickHandler
{
    Enemy enemy;
    [SerializeField] Slider healthSlider;
    TMP_Text enemyNameUI;

    public event Action<GameCharacter> onCharacterClick;
    public event Action<EnemyInfo> onInfoBarDeactive;

    public Slider HealthSlider { get => healthSlider; private set => healthSlider = value; }
    public Enemy Enemy { get => enemy; }

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.onHealthChanged += UpdateHealthText;
        enemy.onCharacterDie += DeactiveHealtBar;
    }

    public void UpdateHealthText()
    {
        if (enemy != null)
        {
            healthSlider.value = enemy.Health / enemy.MaxHealth;
        }

    }

    public void DeactiveHealtBar(GameCharacter character)
    {
        if (enemy != null)
        {
            enemy.onHealthChanged -= UpdateHealthText;
            enemy.onCharacterDie -= DeactiveHealtBar;
            enemy = null;
        }
       
        onInfoBarDeactive?.Invoke(this);
        gameObject.SetActive(false);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onCharacterClick?.Invoke(enemy);
    }
}
