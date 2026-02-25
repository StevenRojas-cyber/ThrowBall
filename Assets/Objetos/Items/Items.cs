using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{
    public string itemName;
    public float itemTrowAngle;
    public float itemTrowVelocity;

    public GameObject itemObject;
    public CircleCollider2D Hitbox;
    public BoxCollider2D itemBodyCollision;

    GameObject User;

    private bool playerInside;
    private Component UserController;

    public enum ItemState
    {
        OnGround,
        Throwed
    }

    public ItemState currentState = ItemState.OnGround;


    private void Awake()
    {

    }

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
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, true);

            User = collision.gameObject;
            UserController = collision.gameObject.GetComponent<Conejo_CharcterController>();
            if (UserController == null) return;

            playerInside = true;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Enable();
        }


        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (name == "Player 2")
        {
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, true);

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
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, false);

            playerInside = false;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }

        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (collision.CompareTag("Zorro_Player"))
        {
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, false);

            playerInside = false;
            UserController.GetComponent<Zorro_CharacterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Balll"))
        {
            if (currentState == ItemState.Throwed)
            {
                StartCoroutine(DespawnItem());
            }
            else
            {
                currentState = ItemState.OnGround;
            }
        }
    }


    private IEnumerator DespawnItem()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
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
