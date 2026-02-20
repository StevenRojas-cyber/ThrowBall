using System.Collections;
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

        Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
        Collider2D itemCol = item.GetComponent<Collider2D>();
        Collider2D playerCol = OwnerPlayer.GetComponent<Collider2D>();

        


        print("Trowing item: " + item.name);
        item.transform.SetParent(null);

        itemRB.simulated = true;
        itemRB.bodyType = RigidbodyType2D.Dynamic;
        itemRB.gravityScale = 1f;


        itemCol.isTrigger = false;


        float angleRadians = item.itemTrowAngle * Mathf.Deg2Rad;
        float direction = Mathf.Sign(OwnerPlayer.transform.localScale.x);

        Vector2 throwDirection = new Vector2(Mathf.Cos(angleRadians) * direction, Mathf.Sin(angleRadians));

        item.transform.position = OwnerPlayer.transform.position + (Vector3)(throwDirection * 0.5f); // Ajusta la posición de lanzamiento según sea necesario

        Physics2D.IgnoreCollision(itemCol, playerCol, true);

        //item.GetComponent<Rigidbody2D>().linearVelocity = OwnerPlayer.transform.right * item.itemTrowVelocity;

        itemRB.linearVelocity = throwDirection * item.itemTrowVelocity;

        
        StartCoroutine(ReenableCollision(itemCol, playerCol, 0.3f));


        CurrentHandState = HandState.Empty;
        OwnerPlayer.GetComponent<Conejo_CharcterController>().trowAction.action.Disable();

    }

    private IEnumerator ReenableCollision(Collider2D itemCol, Collider2D playerCol, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        Physics2D.IgnoreCollision(itemCol, playerCol, false);
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