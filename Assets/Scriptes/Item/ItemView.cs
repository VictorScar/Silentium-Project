using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    //[SerializeField] WeaponRoot weaponRoot;
    //[SerializeField] GameObject armorRoot;

 
    public void ViewWeapon(Item item)
    {
        WeaponRoot useWeapon = GetComponentInChildren<WeaponRoot>();
        if (useWeapon != null)
        {
            Destroy(useWeapon.gameObject);
        }
        Instantiate(item.itemModel, transform.position, transform.rotation, transform);
    }
}
