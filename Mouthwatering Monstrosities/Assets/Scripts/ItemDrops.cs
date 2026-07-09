using UnityEngine;

[CreateAssetMenu]

public class itemDrops : ScriptableObject
{
    public GameObject dropModel;
    public float dropChance;
    public string itemName;

    public itemDrops(string lootName, float dropChance) {
        this.itemName = lootName;
        this.dropChance = dropChance;
    }
    
    
    /*
    enum droppedItem
    {
        GremLeg, GobEar, BugArm, OrcJowl, OgreB, OgreS, OgreH,
        APoultry, WolfSnout, MinoHooves, GriffWings, ChimL, ChimG, ChimS,
        LizTail, NagaChunks, DragbHorns, DrakeRind, DragB, DragO, DragH,
        RotFlesh, BoneM, EctoJelly, VampBlood, LichF, LichH, LichS,
        DeadFl, ArmEscar, DisBrain, DemTendril, BeldS, BeldT, BeldE
    };
    */

}
