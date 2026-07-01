using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorModel;
    [SerializeField] GameObject doorButton;
    [SerializeField] public bool Locked = false;

    bool inTrigger = false;

    public bool hasKey = false;

    private void Start()
    {
        doorButton.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Locked)
        {
            doorModel.SetActive(false);
        }
        else if(other.CompareTag("Player") && hasKey)
        {
            doorButton.SetActive(true);
        }
        inTrigger = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && hasKey && inTrigger)
        {

            UnlockDoor();
            doorModel.SetActive(false);
            doorButton.SetActive(false);
            hasKey = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorModel.SetActive(true);
            doorButton.SetActive(false);
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
