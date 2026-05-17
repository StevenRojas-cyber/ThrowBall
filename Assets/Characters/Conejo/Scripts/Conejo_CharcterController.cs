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

    public ParticleSystem Dust;
    public InputActionReference moveAction;
    public InputActionReference pickUpAction;
    public InputActionReference trowAction;
    public Conejo_Brazo PlayerArm;
    public Animator ConejoAnimator;


    private Vector2 moveDirection;
    private Items currentItemGround;

    Rigidbody2D CharacterBody2D;
    Transform characterTransform;
    

    void Start()
    {
        CharacterBody2D = GetComponent<Rigidbody2D>();
        characterTransform = GetComponent<Transform>();
        
        moveAction.action.Enable();
        pickUpAction.action.Disable();
        trowAction.action.Disable();
    }

    void Update()
    {       
       Movement();
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

       if(moveDirection.x == 0) Dust.Stop();

        //Girar el personaje segun la direccion del movimiento
        if (moveDirection.x > 0)
        {
            CreateDust();
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (moveDirection.x < 0)
        {
        CreateDust();
            transform.localScale = new Vector3(-1, 1, 1);
        }


        //Sistema de Recoger Objetos
        if (pickUpAction.action.WasPressedThisFrame())
        {
            if (currentItemGround != null && PlayerArm.IsHandEmpty())
            {
                currentItemGround.PickUp();

            }
        }

        if (trowAction.action.WasPressedThisFrame())
        {

            ConejoAnimator.SetBool("IsThrowing", true);
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

