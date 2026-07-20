using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]

public class weapon : ScriptableObject
{
    public enum Type { Blade, Blunt, Ranged }
    [SerializeField] public Type type;
    [SerializeField] public float damage;
    [SerializeField] public float range;
    [SerializeField] public float attackSpeed;
    [SerializeField] public Renderer model;
    [SerializeField] public GameObject projectile;
    [SerializeField] public Sprite icon;
    


}