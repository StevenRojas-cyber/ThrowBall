using UnityEngine;
using UnityEngine.InputSystem;

public class Zorro_CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference moveAction;

    private Rigidbody2D rb;
    private Vector2 move;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction.action.Enable();
    }

    void Update()
    {
        move = moveAction.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
    }

}