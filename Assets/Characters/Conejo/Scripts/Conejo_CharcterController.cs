using UnityEngine;
using UnityEngine.InputSystem;

public class Conejo_CharcterController : MonoBehaviour
{
    public float speed = 5.0f;
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public Conejo_Brazo PlayerArm;

    private Vector2 moveDirection;
    private Items currentItem;

    Rigidbody2D CharacterBody2D;

    public bool enabledPickUp = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterBody2D = GetComponent<Rigidbody2D>();
        pickUpAction.action.Disable();
    }



    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();

        if(currentItem != null && pickUpAction.action.WasPressedThisFrame())
        {
            currentItem.PickUp();
        }
    }



    private void FixedUpdate()
    {
        CharacterBody2D.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y*speed);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if(item != null)
        {
            currentItem = item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if(item == currentItem)
        {
            currentItem = null;
        }
    }
}

