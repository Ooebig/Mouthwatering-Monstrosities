using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableNeighbor : MonoBehaviour
{
    [SerializeField] private List<GameObject> myObjects = new List<GameObject>();
    [SerializeField] private GameObject lines;

    public void Start()
    {
        if (!gameObject.GetComponent<UpgradeInfo>().isPurchased && !gameObject.GetComponent<UpgradeInfo>().isAvailable)
        {
            gameObject.SetActive(false);
        }
        lines.SetActive(false);
    }

    // Update is called once per frame
    public void Unlock()
    {
        for(int i = 0; i < myObjects.Count; i++)
        {
            if (myObjects[i] != null)
            {
                myObjects[i].SetActive(true);
            }
        }
        lines.SetActive(true);
    }
}
