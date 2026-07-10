using UnityEngine;
using System.Collections.Generic;
public class LootBag : MonoBehaviour
{
    public List<itemDrops> dropsList = new List<itemDrops>();

    List<itemDrops> GetDroppedItems() {
        int randomNumber = Random.Range(1, 101);
        List<itemDrops> possibleDrops = new List<itemDrops>();
        foreach (itemDrops droppedItem in dropsList)
        {
            if (randomNumber <= droppedItem.dropChance)
            {
                possibleDrops.Add(droppedItem);
            }
        }
        if (possibleDrops.Count > 0)
        {
            return possibleDrops;
        }
        return null;
    }

    public void InstantiateDrops(Vector3 enemyDeathPos) {
        List<itemDrops> droppedItems = GetDroppedItems();
        if (droppedItems != null)
        {
            if (droppedItems.Count == 1)
            {
                GameObject dropGameObject = Instantiate(droppedItems[0].dropModel, enemyDeathPos, Quaternion.identity);
            }
            else if (droppedItems.Count == 2)
            {
                Vector3 dropPos1 = new Vector3(enemyDeathPos.x-1, enemyDeathPos.y, enemyDeathPos.z);
                Vector3 dropPos2 = new Vector3(enemyDeathPos.x+1, enemyDeathPos.y, enemyDeathPos.z);
                GameObject dropGameObject = Instantiate(droppedItems[0].dropModel, dropPos1, Quaternion.identity);
                GameObject secondGameObject = Instantiate(droppedItems[1].dropModel, dropPos2, Quaternion.identity);
            }
            else if (droppedItems.Count == 3)
            {
                Vector3 dropPos1 = new Vector3(enemyDeathPos.x - 1, enemyDeathPos.y, enemyDeathPos.z - 1);
                Vector3 dropPos2 = new Vector3(enemyDeathPos.x + 1, enemyDeathPos.y, enemyDeathPos.z - 1);
                Vector3 dropPos3 = new Vector3(enemyDeathPos.x, enemyDeathPos.y, enemyDeathPos.z + 2);
                GameObject dropGameObject = Instantiate(droppedItems[0].dropModel, dropPos1, Quaternion.identity);
                GameObject secondGameObject = Instantiate(droppedItems[1].dropModel, dropPos2, Quaternion.identity);
                GameObject thirdGameObject = Instantiate(droppedItems[2].dropModel, dropPos3, Quaternion.identity);
            }
            
        }

    }
}
