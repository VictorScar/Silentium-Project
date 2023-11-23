using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntrants : MonoBehaviour
{
    [SerializeField] Player playerPrefab;
    Player playerInstance = null;
    [SerializeField] List<Enemy> enemyPrefabs;
    [SerializeField] List<Enemy> enemiesInstance = new List<Enemy>();
    [SerializeField] GameObject playerSpawn;
    [SerializeField] GameObject[] enemySpawns;

    public event Action<GameCharacter, Vector3, int> onCharacterSpawn;

    private void Start()
    {
        SpawnCharacters(playerPrefab, enemyPrefabs);
    }

    public void SpawnCharacters(Player player, List<Enemy> enemies)
    {
        playerInstance = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation);
        onCharacterSpawn?.Invoke(playerInstance, playerInstance.transform.position + new Vector3(0, 0, 5), 0);
        for (int i = 0; i < enemySpawns.Length; i++)
        {
            if (i < enemyPrefabs.Count)
            {
                enemiesInstance.Add(Instantiate(enemyPrefabs[i], enemySpawns[i].transform.position,
                    enemySpawns[i].transform.rotation));
                onCharacterSpawn?.Invoke(enemiesInstance[i], transform.position, i + 1);
            }
        }
    }
}
