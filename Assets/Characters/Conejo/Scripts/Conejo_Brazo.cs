using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Conejo_Brazo : MonoBehaviour
{

    public GameObject OwnerPlayer;
     
    public Items CurrentItemInHand;

    public enum HandState
    {
        Empty,
        HoldingItem
    }


    public HandState CurrentHandState;

    void Start()
    {
        string name = OwnerPlayer.name;
        //print("Owner Player: " + name);
        CurrentHandState = HandState.Empty;
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
        item.transform.Rotate(0,0,90);
        item.GetComponent<Rigidbody2D>().simulated = false;

        CurrentHandState = HandState.HoldingItem;
        OwnerPlayer.GetComponent<Conejo_CharcterController>().trowAction.action.Enable();

    }

    public void TrowItem(Items item)
    {
        if (item == null) return;

        print("Trowing item: " + item.name);
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody2D>().simulated = true;
        item.GetComponent<Rigidbody2D>().linearVelocity = OwnerPlayer.transform.right * item.itemTrowVelocity;
        
        CurrentHandState = HandState.Empty;
        OwnerPlayer.GetComponent<Conejo_CharcterController>().trowAction.action.Disable();

    }

    public bool IsHandEmpty()
    {
        return CurrentHandState == HandState.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}