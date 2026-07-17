using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text stackSizeText;
    
    public void ClearSlot() {
        itemIcon.SetEnabled(false);
        stackSizeText.text = string.Empty;
    }

    public void DrawSlot(InventoryItem item) { 
        if(item == null)
        {
            ClearSlot();
            return;
        }
        itemIcon.SetEnabled(true);
        itemIcon.sprite = item.itemData.itemIcon;
        stackSizeText.text = item.stackSize.ToString();
    }
}
