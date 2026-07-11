using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class craftingMenu : MonoBehaviour, IInteractable
{
    public List<GameObject> toTurnOff = new List<GameObject>();
    public List<GameObject> toTurnOn = new List<GameObject>();
    public void Interact()
    {
        Debug.Log("almost");
        
    }
}
