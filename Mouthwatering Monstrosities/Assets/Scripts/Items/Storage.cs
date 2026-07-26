using UnityEngine;
using System.Collections.Generic;

public class Storage : MonoBehaviour
{
    public string storageName;
    public int storageSize;
    [HideInInspector] static public bool isStorageOpened;

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
