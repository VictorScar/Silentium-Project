using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    Enemy enemy;
    TMP_Text enemyNameUI;

    public Slider HealthSlider { get => healthSlider; private set => healthSlider = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy!=null)
        {
            healthSlider.value = enemy.Health / enemy.MaxHealth;
        }
    }

    public void InitObject(Enemy enemy)
    {
        this.enemy = enemy;
    }
}
