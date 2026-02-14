using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName;
    public float itemTrowAngle;
    public float itemTrowVelocity;

    public GameObject itemObject;
    public CircleCollider2D Hitbox;
    

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
        string name = collision.gameObject.name;

        //En Caso de que el jugador que entre en el hitbox del item sea el Player 1
        if (name == "Player 1")
        {
            User = collision.gameObject;
            UserController = collision.gameObject.GetComponent<Conejo_CharcterController>();
            if (UserController == null) return;

            playerInside = true;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Enable();
        }


        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (name == "Player 2")
        {
            User = collision.gameObject;
            UserController = collision.gameObject.GetComponent<Zorro_CharacterController>();
            if (UserController == null) return;
            playerInside = true;
            UserController.GetComponent<Zorro_CharacterController>().pickUpAction.action.Enable();
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //En Caso de que el jugador que entre en el hitbox del item sea el Player 1
        if (collision.CompareTag("Conejo_Player"))
        {
            playerInside = false;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }

        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (collision.CompareTag("Zorro_Player"))
        {
            playerInside = false;
            UserController.GetComponent<Zorro_CharacterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }
    }





    public bool CanBePickedUp()
    {
        return playerInside;
    }




    public void PickUp()
    {
        //Debug.Log("Picked up: " + itemName);

        if(User.name == "Player 1")
        {
            UserController.GetComponent<Conejo_CharcterController>().PlayerArm.SetItemInHand(this);
            UserController.GetComponent<Conejo_CharcterController>().PlayerArm.AttachItemHand(this);

        }
        else if(User.name == "Player 2")
        {
            UserController.GetComponent<Zorro_CharacterController>().PlayerArm.SetItemInHand(this);
            UserController.GetComponent<Zorro_CharacterController>().PlayerArm.AttachItemHand(this);
        }
        Hitbox.enabled = false;

    }
}
