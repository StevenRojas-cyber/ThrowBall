using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName;
    public GameObject itemObject;

    public BoxCollider2D Hitbox;
    GameObject User;

    private bool playerInside;
    private Component UserController;
    

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
       UserController = collision.gameObject.GetComponent<Conejo_CharcterController>();
        if (UserController == null) return;

        playerInside = true;
        UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Enable();
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        User = collision.gameObject;
        
        User.GetComponent<Conejo_CharcterController>().pickUpAction.action.Enable();
        User.GetComponent<Conejo_CharcterController>().CheckPickup();

        if (User.GetComponent<Conejo_CharcterController>().pickUpAction.action.ReadValue<float>() > 0)
        {
            PickUp();
        }

    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Conejo_CharcterController>() == UserController)
        {
            playerInside = false;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Disable();
            UserController = null;
        }

    }

    public bool CanBePickedUp()
    {
        return playerInside;
    }

    public void PickUp()
    {
        Debug.Log("Picked up: " + itemName);
        Hitbox.enabled = false;
        this.gameObject.SetActive(false);
    }
}
