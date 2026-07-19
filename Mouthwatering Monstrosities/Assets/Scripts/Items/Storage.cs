using UnityEngine;

public class Storage : MonoBehaviour
{
    public string storageName;
    public int storageSize;
    float timeScaleOrig;
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
        gamemanager.instance.OpenStorageMenu();
    }

    static public void CloseStorage(Storage storage)
    {
        isStorageOpened = false;
        gamemanager.instance.CloseStorageMenu();
    }
}
