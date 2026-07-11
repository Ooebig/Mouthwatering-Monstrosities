using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

public class InventoryManager : MonoBehaviour
{
    
    private void OnEnable()
    {
        PlayerInventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        PlayerInventory.OnInventoryChange -= DrawInventory;
    }

    [SerializeField] private int invSize;
    public GameObject slotPrefab;
    public List<InventorySlot> inventorySlots = new List<InventorySlot>();

    void resetPlayerInventory() {
        foreach (Transform childTransform in transform) {
            Destroy(childTransform.gameObject);
        }
        inventorySlots = new List<InventorySlot>(invSize);
    }

    void DrawInventory(List<InventoryItem> inventory)
    {
        resetPlayerInventory();
        for (int i = 0; i < inventorySlots.Capacity; i++) {
            CreateInventorySlot();
        }
        for (int i = 0; i < inventory.Count; i++) {
            inventorySlots[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot() { 
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);
        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        inventorySlots.Add(newSlotComponent); 
    }
}
