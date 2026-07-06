using UnityEditorInternal;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] GameObject tree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tree.SetActive(false);
    }    
}
