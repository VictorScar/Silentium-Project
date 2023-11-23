using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] TurnManager turnManager;
    [SerializeField] ActionManager actionManager;
    [SerializeField] UIManager uiManager;
    Player player;
    [SerializeField] List <Enemy> enemiesPrefab;
    PlayerStorage playerStorage;
    [SerializeField] ActionCamera actionCamera;
    [SerializeField] AnimationManager animationManager;

    public static Game Instance { get; private set; }
    public ActionManager ActionManager { get => actionManager; }
    public TurnManager TurnManager { get => turnManager; }
    public UIManager UiManager { get => uiManager; }
    public Player Player { get => player; set => player = value; }
    public List<Enemy> EnemiesPrefab { get => enemiesPrefab; set => enemiesPrefab = value; }
    public ActionCamera ActionCamera { get => actionCamera; set => actionCamera = value; }
    public AnimationManager AnimationManager { get => animationManager; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        animationManager.InitControllAnimator(Player);
        Player.onCharacterDie += DestroyEntrants;
    }

    void DestroyEntrants(GameCharacter character)
    {
        if (character is Player)
        {
            Player = null;
        }
    }

}
