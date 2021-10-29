using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numOfKeys;
    public int numOfContainers;

    public void AddItem(Item itemToAdd)
    {
        if (itemToAdd.isKey)
            numOfKeys++;
        else if (itemToAdd.isHeartContainer)
            numOfContainers++;
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
