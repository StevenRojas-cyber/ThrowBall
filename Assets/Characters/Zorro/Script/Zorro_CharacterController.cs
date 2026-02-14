using UnityEngine;
using UnityEngine.InputSystem;

public class Zorro_CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;

    public Zorro_Brazo PlayerArm;

    private Rigidbody2D rb;
    private Vector2 move;

    private Items currentItem;
    public bool enabledPickUp = false;
  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction.action.Enable();
    }

    void Update()
    {
        move = moveAction.action.ReadValue<Vector2>();

        if (currentItem != null && pickUpAction.action.WasPressedThisFrame())
        {
            currentItem.PickUp();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if (item != null)
        {
            currentItem = item;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Items item = collision.GetComponent<Items>();
        if (item == currentItem)
        {
            currentItem = null;
        }
    }

}