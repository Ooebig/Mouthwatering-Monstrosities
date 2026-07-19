using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public List<InventoryItem> playerInv = new List<InventoryItem>();
    private Dictionary<ItemDrops, InventoryItem> playerItemDictionary = new Dictionary<ItemDrops, InventoryItem>();

    private void OnEnable()
    {
        Collectible.OnCollected += Add;
    }

    private void OnDisable()
    {
        Collectible.OnCollected -= Add;
    }

    public void Add(ItemDrops itemData) {
        if (playerItemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            OnInventoryChange?.Invoke(playerInv);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            playerInv.Add(newItem);
            playerItemDictionary.Add(itemData, newItem);
            OnInventoryChange?.Invoke(playerInv);
        }
    }

    public void Remove(ItemDrops itemData) {
        if (playerItemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0) {
                playerInv.Remove(item);
                playerItemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(playerInv);
        }
    }

}
