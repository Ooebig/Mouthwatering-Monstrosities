using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu]

public class ItemDrops : ScriptableObject
{
    public GameObject dropModel;
    public int dropChance;
    public string itemName;
    public Sprite itemIcon;

    public ItemDrops(string lootName, int dropChance) {
        this.itemName = lootName;
        this.dropChance = dropChance;
    }
}
