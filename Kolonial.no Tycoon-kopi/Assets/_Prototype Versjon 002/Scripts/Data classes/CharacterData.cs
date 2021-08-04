using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Object/New Character object")]
public class CharacterData : ScriptableObject
{
    new public string name = "New character";
    public Sprite portrait;
    [Range( 0,999)]
    public int fatigue, progress, motivationStat, speedStat, researchStat, qualityStat, designStat ;
    
    public enum Job
    {
        WarehouseWorker,
        Driver,
        Unassigned
    }
    public Job AssignedJob = Job.Unassigned;
}
