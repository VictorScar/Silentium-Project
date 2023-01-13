using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] ActionManager actionManager;
    [SerializeField] UIManager uiManager;
    Player player;
    [SerializeField] List <Enemy> enemies;
    PlayerStorage playerStorage;

    public static Game Instance { get; private set; }
    public ActionManager ActionManager { get => actionManager; }
    public TurnManager TurnManager { get => turnManager; }
    public UIManager UiManager { get => uiManager; }
    public Player Player { get => player; set => player = value; }
    public List<Enemy> Enemies { get => enemies; set => enemies = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }
}
