using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;


public class InventorySlot : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text stackSizeText;

    public void ClearSlot() {
        itemNameText.text = string.Empty;
        stackSizeText.text = string.Empty;
    }

    public void DrawSlot(InventoryItem item) { 
        if(item == null)
        {
            ClearSlot();
            return;
        }
        itemNameText.text = item.itemData.itemName;
        stackSizeText.text = item.stackSize.ToString();

    }
}
