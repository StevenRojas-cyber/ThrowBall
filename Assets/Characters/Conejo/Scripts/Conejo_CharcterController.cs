using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class Conejo_CharcterController : MonoBehaviour
{
    public bool enabledPickUp = false; 
    public float speed = 5.0f;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Conejo_Brazo PlayerArm;

    private Vector2 moveDirection;
    private Items currentItemGround;

    Rigidbody2D CharacterBody2D;


    void Start()
    {
        CharacterBody2D = GetComponent<Rigidbody2D>();
        pickUpAction.action.Disable();
        trowAction.action.Disable();

    }

    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();


        if(pickUpAction.action.WasPressedThisFrame())
        {

            if (currentItemGround != null && PlayerArm.IsHandEmpty())
            {
                currentItemGround.PickUp();
                
            }
        }

        if(trowAction.action.WasPressedThisFrame())
        {
            if (PlayerArm.CurrentItemInHand != null && PlayerArm.IsHandEmpty() == false)
            {
                PlayerArm.TrowItem(PlayerArm.CurrentItemInHand);
                
            }
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
            currentItemGround = item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if(item == currentItemGround)
        {
            currentItemGround = null;
        }
    }
}

