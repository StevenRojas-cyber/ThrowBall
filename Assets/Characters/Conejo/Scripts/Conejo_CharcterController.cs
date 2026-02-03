using UnityEngine;
using UnityEngine.InputSystem;

public class Conejo_CharcterController : MonoBehaviour
{
    public float speed = 5.0f;
    public InputActionReference moveAction;

    private Vector2 moveDirection;

    Rigidbody2D CharacterBody2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CharacterBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        CharacterBody2D.linearVelocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }
}
