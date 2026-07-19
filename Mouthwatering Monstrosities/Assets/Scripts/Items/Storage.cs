using UnityEngine;

public class Storage : MonoBehaviour
{
    public string storageName;
    public int storageSize;
    [HideInInspector] static public bool isStorageOpened;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    static public void OpenStorage(Storage storage)
    {
        isStorageOpened = true;

    }

    static public void CloseStorage(Storage storage)
    {
        isStorageOpened = false;
    }
}
