using UnityEngine;
using UnityEngine.InputSystem;

public class Zorro_CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Animator ZorroAnimator;

    public Zorro_Brazo PlayerArm;

    private Rigidbody2D rb;
    private Vector2 move;

    private Items currentItemGround;
    public bool enabledPickUp = false;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction.action.Enable();
        pickUpAction.action.Disable();
        trowAction.action.Disable();
    }

    void Update()
    {
        move = moveAction.action.ReadValue<Vector2>();

        ZorroAnimator.SetFloat("movement", Mathf.Abs(move.x));

        if (move.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        if (pickUpAction.action.WasPressedThisFrame())
        {

            if (currentItemGround != null && PlayerArm.IsHandEmpty())
            {
                currentItemGround.PickUp();

            }
        }

        if (trowAction.action.WasPressedThisFrame())
        {
            if (PlayerArm.CurrentItemInHand != null && PlayerArm.IsHandEmpty() == false)
            {
                PlayerArm.TrowItem(PlayerArm.CurrentItemInHand);
               
            }
        }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * speed, -rb.gravityScale);
        rb.SetRotation(0);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if (item != null)
        {
            currentItemGround = item;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if (item == currentItemGround)
        {
            currentItemGround = null;
        }
    }



}