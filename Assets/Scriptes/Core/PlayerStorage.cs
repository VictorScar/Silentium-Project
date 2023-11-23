using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : MonoBehaviour
{
    static Player playerInstance;
    [SerializeField] Player playerPrefab;

    public Player PlayerPrefab { get => playerPrefab; set => playerPrefab = value; }

    private void Awake()
    {
        playerInstance = PlayerPrefab;
    }
    
    
    public Player GetPlayerInstance()
    {
        return playerInstance;
    }
}

