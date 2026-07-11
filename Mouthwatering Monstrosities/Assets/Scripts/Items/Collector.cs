using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You hit something");
        //if (other.isTrigger) return;
        Debug.Log("Its not a trigger");
        ICollectible collectible = other.GetComponent<ICollectible>();
        if (collectible != null) 
        {
            Debug.Log("Its an item");
            collectible.Collect();
        }
    }
}
