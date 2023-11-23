using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] int size = 0;
    [SerializeField] InventorySlot[] slots;
    [SerializeField] List<Item> useItems = new List<Item>();
    Weapon mainWeaponSlot;
    Item secondaryWeaponSlot;
    Item armorSlot;

    [SerializeField] ItemView view;

    public Weapon MainWeaponSlot { get => mainWeaponSlot; set => mainWeaponSlot = value; }

    public event Action onInventoryUpdated;

    private void Awake()
    {
        
    }

    private void Start()
    {
        foreach (InventorySlot slot in slots)
        {
            UseItem(slot.Item);
        }
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

    public void UseItem(Item item)
    {
        useItems.Add(item);
        if (item is Weapon weapon)
        {
            mainWeaponSlot = weapon;
            view.ViewWeapon(weapon);
            onInventoryUpdated?.Invoke();
        }
    }

    public void RemoveItem(Item item)
    {

    }

    public void RedrawEquipment()
    {

    }
}
