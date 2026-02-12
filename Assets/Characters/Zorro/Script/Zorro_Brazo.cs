using UnityEngine;

public class Zorro_Brazo : MonoBehaviour
{
    public GameObject OwnerPlayer;
    Items CurrentItemInHand;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetItemInHand(Items item)
    {
        CurrentItemInHand = item;

    }

    public void AttachItemHand(Items item)
    {
        if (item == null) return;
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;
    }

}
