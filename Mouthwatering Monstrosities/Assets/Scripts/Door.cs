using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorModel;
    [SerializeField] public bool Locked = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Locked)
        {
            doorModel.SetActive(false);
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorModel.SetActive(true);
        }
    }

    public void UnlockDoor()
    {
        Locked = false;
    }

    public void LockDoor()
    {
        Locked = true;
    }

}