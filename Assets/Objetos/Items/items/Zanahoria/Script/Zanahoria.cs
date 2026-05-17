using Unity.VisualScripting;
using UnityEngine;

public class Zanahoria : Items
{
    void Awake()
    {
        itemName = itemData.itemName;
        itemTrowAngle = itemData.itemTrowAngle;
        itemTrowVelocity = itemData.itemTrowVelocity;
        itemDespawnTime = itemData.itemDespawnTime;
    }

    private void Start()
    {

    }


    private void Update()
    {
   

    }
}
