using UnityEditorInternal;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    [SerializeField] public List<GameObject> tree;

    [SerializeField] GameObject Arrows;

    [SerializeField] int level;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject node in tree)
        {
            node.SetActive(false);
        }

    }
    public void OpenTree()
    {
        tree[0].SetActive(true);
        if (level > 1)
        {
            Arrows.SetActive(true);
        }
    }
}
