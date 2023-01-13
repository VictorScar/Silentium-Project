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

    private void Start()
    {
        SpawnCharacters(playerPrefab, enemyPrefabs);
    }

    public void SpawnCharacters(Player player, List<Enemy> enemies)
    {
        playerInstance = Instantiate(player, playerSpawn.transform.position, playerSpawn.transform.rotation);

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            if (i < enemyPrefabs.Count)
            {
                enemiesInstance.Add(Instantiate(enemyPrefabs[i], enemySpawns[i].transform.position,
                    enemySpawns[i].transform.rotation));
            }

        }

    }
}
