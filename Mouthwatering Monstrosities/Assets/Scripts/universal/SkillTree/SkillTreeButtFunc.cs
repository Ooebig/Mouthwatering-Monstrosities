using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SkillTreeButtonFunctions : MonoBehaviour
{ 
    UpgradeInfo upgradeInfo;


    public void Upgrade(Button upgrade)
    {

        UpgradeInfo upgradeInfo = upgrade.GetComponent<UpgradeInfo>();

        if (upgradeInfo == null) return;

        if (upgradeInfo.isPurchased) return;
        //uncomment when player gets a currency System

        /*if(gamemanager.instance.Gold >= Gold)
        {
            gamemanager.instance.Gold -= Gold;
            upgradeInfo.isPurchased = true;
        }
        else return;*/


        switch (upgradeInfo.upgradeType)
        {
            case UpgradeInfo.UpgradeType.Health:
                break;

            case UpgradeInfo.UpgradeType.Damage:
                break;

            case UpgradeInfo.UpgradeType.Speed:
                break;

            case UpgradeInfo.UpgradeType.AttackSpeed:
                break;

            case UpgradeInfo.UpgradeType.CritChance:
                break;

            case UpgradeInfo.UpgradeType.CritDamage:
                break;

            default:
                break;
        }
        upgradeInfo.isPurchased = true;
        upgrade.GetComponent<Image>().color = Color.green;
        upgrade.GetComponent<AvailableNeighbor>().Unlock();
    }

}