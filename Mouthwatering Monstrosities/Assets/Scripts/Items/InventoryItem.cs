using UnityEngine;
using System;

[Serializable]

public class InventoryItem
{
    public ItemDrops itemData;
    public int stackSize;

    public InventoryItem(ItemDrops item)
    {
        itemData = item;
        AddToStack();
    }

    public void AddToStack(int addition = 1) {
        stackSize = stackSize + addition;
    }
    public void RemoveFromStack(int removal = 1) {
        stackSize = stackSize - removal;
    }
}
