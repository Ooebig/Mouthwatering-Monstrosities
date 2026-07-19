using UnityEngine;

public class UpgradeInfo : MonoBehaviour
{

    [SerializeField][Range(0, 100)] public int GoldCost;
    [SerializeField][Range(0, 1000)] public int AmountToIncrease;


    [SerializeField] public UpgradeType upgradeType;
    [SerializeField] public bool isAvailable;

    public bool isPurchased;
    public enum UpgradeType
    {
        Health,
        Damage,
        Speed,
        AttackSpeed,
        JumpSpeed,
        JumpHeight,

        NA
    }
}