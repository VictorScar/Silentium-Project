using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class CharacterInventory : MonoBehaviour
{
    [SerializeField] int size = 0;
    [SerializeField] InventorySlot[] slots;
    [SerializeField] List <InventorySlot> useSlots = new List<InventorySlot>();


    private void Awake()
    {
        slots = new InventorySlot[size];
    }

    public void AddItem(Item item, int count)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i].Item = item;
                slots[i].Count = count;
            }
        }
    }


}
