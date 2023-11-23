using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPointUI : MonoBehaviour
{
    [SerializeField] List<GameObject> points = new List<GameObject>();
    [SerializeField] Player player;

    public void Init(Player player)
    {
        this.player = player;
        player.onAbilityPointCountChanged += UpadateAbilityPoints;
    }

    void UpadateAbilityPoints(int pointCount)
    {
        Debug.Log("Update AbilityPoint");
        for (int i = 0; i < points.Count; i++)
        {
            if (i < pointCount)
            {
                points[i].SetActive(true);
            }
            else if (points[i].activeInHierarchy)
            {
                points[i].SetActive(false);
            }
            continue;
        }
    }

    //IEnumerator InitUI()
    //{
    //    yield return new WaitUntil(() => Game.Instance.Player != null);
    //    player = Game.Instance.Player;
    //    player.onAbilityPointCountChanged += UpadateAbilityPoints;
    //}
}
