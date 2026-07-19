using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SkillTreeButtonFunctions : MonoBehaviour
{ 
    UpgradeInfo upgradeInfo;
    [SerializeField] SkillTree skillTree;
    List<GameObject> trees;
    int treeIndex = 0;

    public void Start()
    {
        trees = skillTree.tree;
    }

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
                gamemanager.instance.playerScript.originalHP += upgradeInfo.AmountToIncrease;
                break;

            case UpgradeInfo.UpgradeType.Damage:
                for(int i = 0; i < gamemanager.instance.playerScript.weaponList.Length; i++)
                {
                    gamemanager.instance.playerScript.weaponList[i].damage += upgradeInfo.AmountToIncrease;
                }
                break;

            case UpgradeInfo.UpgradeType.Speed:
                gamemanager.instance.playerScript.speed += upgradeInfo.AmountToIncrease;
                break;

            case UpgradeInfo.UpgradeType.AttackSpeed:
                gamemanager.instance.playerScript.attackRate += upgradeInfo.AmountToIncrease;
                break;

            case UpgradeInfo.UpgradeType.JumpSpeed:
                gamemanager.instance.playerScript.jumpSpeed += upgradeInfo.AmountToIncrease;
                break;

            case UpgradeInfo.UpgradeType.JumpHeight:
                gamemanager.instance.playerScript.jumpMax += upgradeInfo.AmountToIncrease;
                break;

            default:
                break;
        }
        upgradeInfo.isPurchased = true;
        upgrade.GetComponent<Image>().color = Color.cornsilk;
        upgrade.GetComponent<AvailableNeighbor>().Unlock();
    }

    public void NextSkillTree()
    {
        trees[treeIndex].SetActive(false);
        treeIndex += 1;
        if (treeIndex >= trees.Count)
        {
            treeIndex = 0;
        }
        for (int i = 0; i < trees.Count; i++)
        {
            if (i == treeIndex)
            {
                trees[i].SetActive(true);
            }
            else
            {
                trees[i].SetActive(false);
            }
        }
    }
    public void PrevSkillTree()
    {
        treeIndex -= 1;
        if (treeIndex < 0)
        {
            treeIndex = trees.Count - 1;
        }
        for (int i = 0; i < trees.Count; i++)
        {
            if (i == treeIndex)
            {
                trees[i].SetActive(true);
            }
            else
            {
                trees[i].SetActive(false);
            }
        }
    }

    public void Unpause()
    {

        gamemanager.instance.stateUnpause();

    }
    public void quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}