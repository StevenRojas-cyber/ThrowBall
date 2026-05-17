using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zorro_CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public bool isGameEnded = false;
    public bool enabledPickUp = false;
    
    public ParticleSystem Dust;
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Animator ZorroAnimator;

    public Zorro_Brazo PlayerArm;

    public enum PlayerState
    {
        Movement,
        ScoreEvent,
    }


    private Rigidbody2D rb;
    private Vector2 move;
    private Items currentItemGround;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction.action.Enable();
        pickUpAction.action.Disable();
        trowAction.action.Disable();
    }

    void Update()
    {
        MovementState();
    }

    public void Celebration(bool ImScored)
    {
        ZorroAnimator.SetTrigger("AnyoneScored");

        moveAction.action.Disable();

        if (ImScored)
        {
            ZorroAnimator.SetBool("GotScore", true);
            ZorroAnimator.SetBool("EnemyScore", false);
            PlayerArm.gameObject.SetActive(false);
        }
        else
        {
            ZorroAnimator.SetBool("GotScore", false);
            ZorroAnimator.SetBool("EnemyScore", true);
            PlayerArm.gameObject.SetActive(false);
        }

    }


    public void StopCelebration()
    {
        if (isGameEnded) return;
        moveAction.action.Enable();

        ZorroAnimator.ResetTrigger("AnyoneScored");
        ZorroAnimator.SetBool("GotScore", false);
        ZorroAnimator.SetBool("EnemyScore", false);
        PlayerArm.gameObject.SetActive(true);
    }


    public void MovementState()
    {
        move = moveAction.action.ReadValue<Vector2>();

        ZorroAnimator.SetFloat("movement", Mathf.Abs(move.x));

        if(move.x == 0) Dust.Stop();

        if (move.x > 0)
        {
            CreateDust();

            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move.x < 0)
        {
            CreateDust();
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
            ZorroAnimator.SetBool("IsThrowing", true);
        }
    }

    void CreateDust()
    {
        Dust.Play();
    }   

    public void ThrowItemAction()
    {
        if (PlayerArm.CurrentItemInHand != null && PlayerArm.IsHandEmpty() == false)
        {
            PlayerArm.TrowItem(PlayerArm.CurrentItemInHand);
        }
        ZorroAnimator.SetBool("IsThrowing", false);
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