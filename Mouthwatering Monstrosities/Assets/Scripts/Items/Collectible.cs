using UnityEngine;
using System;

public class Collectible : MonoBehaviour, ICollectible
{
    public static event itemCollection OnCollected;
    public delegate void itemCollection(ItemDrops item);
    public ItemDrops itemData;

    public void Collect()   
    {
        Destroy(gameObject);
        OnCollected?.Invoke(itemData);

    }
}
