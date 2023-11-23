using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionPointCountUI : MonoBehaviour
{
    [SerializeField] TMP_Text actionPointsView;
    ActionManager actionManager;

    public void Init(ActionManager actionManager)
    {
        this.actionManager = actionManager;
        actionManager.onActionPointUpdated += UpdateActionPointCount;
    }

    void UpdateActionPointCount(int points)
    {
        actionPointsView.text = points.ToString();
    }
}
