using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public float itemTrowAngle;
    public float itemTrowVelocity;
    public float itemDespawnTime;
}
