using UnityEngine;
using UnityEngine.InputSystem;

public class Zorro_CharacterController : MonoBehaviour
{
    public float speed = 5f;
    public bool isGameEnded = false;
    public bool enabledPickUp = false;
    
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Animator ZorroAnimator;

    public Zorro_Brazo PlayerArm;


    [Header("Particles")]
    [SerializeField] private ParticleSystem Dust;

    private ParticleSystem.EmissionModule DustEmission;

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
        DustEmission = Dust.emission;
    }

    void Update()
    {
        MovementState();
        CreateDust();
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
            if (PlayerArm == null) return;
            if (currentItemGround != null && PlayerArm.IsHandEmpty())
            {
                currentItemGround.PickUp();

            }
        }

        if (trowAction.action.WasPressedThisFrame())
        {
            if (PlayerArm == null) return;
            ZorroAnimator.SetBool("IsThrowing", true);
        }
    }

    void CreateDust()
    {
        if (move.x != 0)
        { 
            DustEmission.rateOverTime = 3f;
        }
        else DustEmission.rateOverTime = 0f;
        
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