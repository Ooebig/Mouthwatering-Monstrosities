using UnityEngine;

public class Crafting : MonoBehaviour
{

    [HideInInspector] static public bool isCraftingOpened;
    static public void OpenCrafting(Crafting crafting)
    {
        isCraftingOpened = true;
        gamemanager.instance.OpenCraftingMenu();
    }

    static public void CloseCrafting(Crafting crafting)
    {
        isCraftingOpened = false;
        gamemanager.instance.CloseCraftingMenu();
    }
}
