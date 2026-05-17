using UnityEngine;

public class Zapallo : Items
{
    private void Awake()
    {
        itemName = itemData.itemName;
        itemTrowAngle = itemData.itemTrowAngle;
        itemTrowVelocity = itemData.itemTrowVelocity;
        itemDespawnTime = itemData.itemDespawnTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
