using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;


public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMesh stackSizeText;

    public void ClearSlot() {
        icon.SetEnabled(false);
        stackSizeText.text = string.Empty;
    }

    public void DrawSlot(InventoryItem item) { 
        if(item == null)
        {
            ClearSlot();
            return;
        }
        icon.SetEnabled(true);
        stackSizeText.text = string.Empty;
        icon.sprite = item.itemData.itemIcon;
        stackSizeText.text = item.stackSize.ToString();

    }
}
