using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    [SerializeField] Item[] itemsModels;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitPool()
    {
        foreach (var item in itemsModels)
        {
            GameObject model = item.itemModel;
            Instantiate(model);
            model.SetActive(false);
        }
        
      
    }

    void DrawModel(Item item)
    {

    }
}
