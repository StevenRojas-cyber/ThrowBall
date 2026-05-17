using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Conejo_CharcterController : MonoBehaviour
{
    public bool GameEnded = false;
    public bool enabledPickUp = false; 
    public float speed = 5.0f;
    public enum PlayerState
    {
        Movement,
        ScoreEvent,
    }

    [Header("Components")]
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Conejo_Brazo PlayerArm;
    public Animator ConejoAnimator;

    [Header("Particles")]
    [SerializeField] private ParticleSystem Dust;

    private Vector2 moveDirection;
    private Items currentItemGround;
    private ParticleSystem.EmissionModule DustEmission;

    Rigidbody2D CharacterBody2D;
    Transform characterTransform;
    

    void Start()
    {
        CharacterBody2D = GetComponent<Rigidbody2D>();
        characterTransform = GetComponent<Transform>();
        DustEmission = Dust.emission;

        moveAction.action.Enable();
        pickUpAction.action.Disable();
        trowAction.action.Disable();
    }

    void Update()
    {       
       Movement();
       CreateDust();
    }

    public void Celebration(bool ImScored)
    {
        ConejoAnimator.SetTrigger("AnyoneScore");

        moveAction.action.Disable();

        if (ImScored)
        {
            ConejoAnimator.SetBool("GotScore", true);
            ConejoAnimator.SetBool("EnemyScore", false);
            PlayerArm.gameObject.SetActive(false);
        }
        else
        {
            ConejoAnimator.SetBool("GotScore", false);
            ConejoAnimator.SetBool("EnemyScore", true);
            PlayerArm.gameObject.SetActive(false);
        }
        
    }


    public void StopCelebration()
    {
        if (GameEnded) return;

        moveAction.action.Enable();

        ConejoAnimator.ResetTrigger("AnyoneScore");
        ConejoAnimator.SetBool("GotScore", false);
        ConejoAnimator.SetBool("EnemyScore", false);
        PlayerArm.gameObject.SetActive(true);
    }


    public void Movement()
    {
        //Sistema de Movimiento
        moveDirection = moveAction.action.ReadValue<Vector2>();

        ConejoAnimator.SetFloat("movement", Mathf.Abs(moveDirection.x));
        

        //Girar el personaje segun la direccion del movimiento
        if (moveDirection.x > 0)
        {
         
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (moveDirection.x < 0)
        {
        
            transform.localScale = new Vector3(-1, 1, 1);
        }


        //Sistema de Recoger Objetos
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
            ConejoAnimator.SetBool("IsThrowing", true);
        }
    }

    void CreateDust()
    {
        if(moveDirection.x != 0)
        {
            DustEmission.rateOverTime = 3;
        }
        else DustEmission.rateOverTime = 0;

    }

    public void ThrowItemAction()
    {
        

        if (PlayerArm.CurrentItemInHand != null && PlayerArm.IsHandEmpty() == false)
        {
            PlayerArm.TrowItem(PlayerArm.CurrentItemInHand);

        }
            ConejoAnimator.SetBool("IsThrowing", false);
    }
  

    private void FixedUpdate()
    {
        CharacterBody2D.linearVelocity = new Vector2(moveDirection.x * speed, -CharacterBody2D.gravityScale);
        
        CharacterBody2D.SetRotation(0);
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

