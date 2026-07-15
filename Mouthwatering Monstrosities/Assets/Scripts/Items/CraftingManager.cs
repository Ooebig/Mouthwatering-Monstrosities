using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.UI;

public class CraftingManager : MonoBehaviour
{
    private InventorySlot currentSlot;
    //public Image customCursor;

    public InventorySlot[] craftingSlots;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            if (currentSlot != null) {
                InventorySlot nearestSlot = null;
                float shortestDistance = float.MaxValue;
                foreach (InventorySlot slot in craftingSlots) {
                    float dist = Vector2.Distance(Input.mousePosition, slot.transform.position);

                    if (dist < shortestDistance) {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                //BETA -- nearestSlot.GetComponent<Image>().sprite = currentSlot.GetComponent
                nearestSlot = currentSlot;
                //currentSlot.stackSizeText = ((int.Parse(currentSlot.stackSizeText)) - 1).ToString();
                currentSlot = null;
            }
        }
    }
    public void OnMouseDownItem(InventorySlot invSlot)
    {
        if (currentSlot == null) {
            currentSlot = invSlot;
            //customCursor.gameObject.SetActive(true);
            //BETA -- customCursor.sprite = currentSlot.itemIcon;

        }
    }
}
