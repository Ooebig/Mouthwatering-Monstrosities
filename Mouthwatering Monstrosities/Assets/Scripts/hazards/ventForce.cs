using UnityEngine;

public class ventForce : MonoBehaviour
{
    [SerializeField] int newGravity = -15;
    private playerController player;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<playerController>();

        if (player != null)
        {
            player.gravity = newGravity;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<playerController>();

        if (player != null)
        {
            player.gravity = 20;
        }
    }
}