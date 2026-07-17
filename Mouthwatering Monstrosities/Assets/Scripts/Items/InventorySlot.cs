using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon;
    public TMP_Text stackSizeText;
    
    public void ClearSlot() {
        Color c = itemIcon.color;
        c.a = 0.0f;
        itemIcon.color = c;
        stackSizeText.text = string.Empty;
    }

    public void DrawSlot(InventoryItem item) { 
        if(item == null)
        {
            ClearSlot();
            return;
        }
        Color c = itemIcon.color;
        c.a = 1.0f;
        itemIcon.color = c;
        itemIcon.sprite = item.itemData.itemIcon;
        stackSizeText.text = item.stackSize.ToString();
    }
}
