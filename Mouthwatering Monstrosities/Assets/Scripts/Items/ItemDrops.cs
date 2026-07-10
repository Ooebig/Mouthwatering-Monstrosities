using UnityEngine;

[CreateAssetMenu]

public class itemDrops : ScriptableObject
{
    public GameObject dropModel;
    public int dropChance;
    public string itemName;

    public itemDrops(string lootName, int dropChance) {
        this.itemName = lootName;
        this.dropChance = dropChance;
    }
}
