using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        Debug.Log("You collected an item");
    }
}
