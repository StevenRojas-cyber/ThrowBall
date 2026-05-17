using UnityEngine;

public class Ladrillo_PickUp : Items
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        itemName = itemData.itemName;
        itemTrowAngle = itemData.itemTrowAngle;
        itemTrowVelocity = itemData.itemTrowVelocity;
        itemDespawnTime = itemData.itemDespawnTime; ;
    }
    
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
