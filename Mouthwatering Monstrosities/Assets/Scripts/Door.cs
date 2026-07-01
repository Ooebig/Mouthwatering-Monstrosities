using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject doorModel;
    [SerializeField] GameObject doorButtons;

    bool playerInTrigger;

    // Update is called once per frame
    void Update()
    {
        if(playerInTrigger)
        {
            if(Input.GetButtonDown("Interact"))
            {
                doorModel.SetActive(false);
                doorButtons.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInTrigger = true;
            doorButtons.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            doorModel.SetActive(true);
            doorButtons.SetActive(false);
        }
    }
}
