using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if (other.isTrigger) return;
        ICollectible collectible = other.GetComponent<ICollectible>();
        if (collectible != null) 
        {
            collectible.Collect();
        }
    }
}
