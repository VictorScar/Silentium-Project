using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] Item item;
    [SerializeField] int count;

    public Item Item { get => item; set => item = value; }
    public int Count { get => count; set => count = value; }
}
