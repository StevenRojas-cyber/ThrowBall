using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Items : MonoBehaviour
{

    [Header("Item Settings")]
     public string itemName;
     public float itemTrowAngle;
     public float itemTrowVelocity;
     public float itemDespawnTime;

    public ItemData itemData;

    public GameObject itemObject;
    public CircleCollider2D Hitbox;
    public BoxCollider2D itemBodyCollision;
    public Rigidbody2D itemRigidBody;

    public ItemSpawner spawner;

    GameObject User;

    private bool playerInside;
    private Component UserController;

    public enum ItemState
    {
        OnGround,
        OnHand,
        Throwed
    }


    public ItemState currentState = ItemState.OnGround;
    
    public void Collect()
    {
        spawner.NotifyItemCollected(gameObject);
        //Destroy(gameObject);
    }

    void Start()
    {
        if (itemData != null)
        {
            itemName = itemData.itemName;
            itemTrowAngle = itemData.itemTrowAngle;
            itemTrowVelocity = itemData.itemTrowVelocity;
            itemDespawnTime = itemData.itemDespawnTime;
        }
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
            //Ignorar colision entre el item y el jugador para evitar problemas al recogerlo
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, true);


            //Asignar el jugador que entro en el hitbox del item a la variable User y obtener su controlador para habilitar la accion de recoger el item
            User = collision.gameObject;
            UserController = collision.gameObject.GetComponent<Conejo_CharcterController>();
            if (UserController == null) return;

            playerInside = true;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Enable();
        
        }


        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (name == "Player 2")
        {
            //Ignorar colision entre el item y el jugador para evitar problemas al recogerlo
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, true);


            //Asignar el jugador que entro en el hitbox del item a la variable User y obtener su controlador para habilitar la accion de recoger el item
            User = collision.gameObject;
            UserController = collision.gameObject.GetComponent<Zorro_CharacterController>();
            if (UserController == null) return;
            playerInside = true;
            UserController.GetComponent<Zorro_CharacterController>().pickUpAction.action.Enable();
        }

        if(name == "Ball")
        {
            //
            Collider2D itemBody = itemBodyCollision;
            Collider2D ballCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, ballCol, true);
        }

    }

   
    private void OnTriggerExit2D(Collider2D collision)
    {
        //En Caso de que el jugador que entre en el hitbox del item sea el Player 1
        if (collision.CompareTag("Conejo_Player"))
        {
            //Dejar de ignorar colision entre el item y el jugador para evitar problemas al recogerlo
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, false);


            //Desasignar el jugador que salio del hitbox del item de la variable User y deshabilitar la accion de recoger el item
            playerInside = false;
            UserController.GetComponent<Conejo_CharcterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }

        //En Caso de que el jugador que entre en el hitbox del item sea el Player 2
        if (collision.CompareTag("Zorro_Player"))
        {
            //Dejar de ignorar colision entre el item y el jugador para evitar problemas al recogerlo
            Collider2D itemBody = itemBodyCollision;
            Collider2D playerCol = collision.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(itemBody, playerCol, false);


            //Desasignar el jugador que salio del hitbox del item de la variable User y deshabilitar la accion de recoger el item
            playerInside = false;
            UserController.GetComponent<Zorro_CharacterController>().pickUpAction.action.Disable();
            UserController = null;
            User = null;
            return;
        }

        if (collision.CompareTag("Balll"))
        {
                Collider2D itemBody = itemBodyCollision;
                Collider2D ballCol = collision.GetComponent<Collider2D>();
                Physics2D.IgnoreCollision(itemBody, ballCol, false);
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
            
        }

    }


    private IEnumerator DespawnItem()
    {
        yield return new WaitForSeconds(itemDespawnTime);
        Destroy(this.gameObject);
    }

    public bool CanBePickedUp()
    {
        return playerInside;
    }

    

    public void PickUp()
    {
        Debug.Log("Picked up: " + itemName);

        if(User.name == "Player 1")
        {
            UserController.GetComponent<Conejo_CharcterController>().PlayerArm.SetItemInHand(this);
            UserController.GetComponent<Conejo_CharcterController>().PlayerArm.AttachItemHand(this);

            currentState = ItemState.OnHand;

            //print("Item State: " + currentState + " picked up by: " + User.name);


            //Reportar al spawner que el item ha sido recogido para eliminarlo de la lista de items en el suelo y evitar problemas con el spawn
            Collect();
        }
        else if(User.name == "Player 2")
        {
            UserController.GetComponent<Zorro_CharacterController>().PlayerArm.SetItemInHand(this);
            UserController.GetComponent<Zorro_CharacterController>().PlayerArm.AttachItemHand(this);
            
            currentState = ItemState.OnHand;

            //print("Item State: " + currentState + " picked up by: " + User.name);

            //Reportar al spawner que el item ha sido recogido para eliminarlo de la lista de items en el suelo y evitar problemas con el spawn
            Collect();
        }
        Hitbox.enabled = false;

    }
}
