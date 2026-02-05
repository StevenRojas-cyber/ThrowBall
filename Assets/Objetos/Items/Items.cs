using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName;
    public GameObject itemObject;
 
    void Start()
    {
        PrintName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void PrintName()
    {
        if(itemName == null) return;
        Debug.Log("Item: " + itemName);
    }
}
