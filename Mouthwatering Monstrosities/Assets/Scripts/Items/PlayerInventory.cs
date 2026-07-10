using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> playerInv = new List<InventoryItem>();
    private Dictionary<ItemDrops, InventoryItem> playerItemDictionary = new Dictionary<ItemDrops, InventoryItem>();

    private void OnEnable()
    {
        Debug.Log("Invoked");
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
            Debug.Log($"Added {item.itemData.name}, total stack is now {item.stackSize}!");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            playerInv.Add(newItem);
            playerItemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.name} to the inventory for the first time!");
        }
        Debug.Log("Passed through Add");
    }

    public void Remove(ItemDrops itemData) {
        if (playerItemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            if (item.stackSize == 0) {
                playerInv.Remove(item);
                playerItemDictionary.Remove(itemData);
            }
        }
    }
}
