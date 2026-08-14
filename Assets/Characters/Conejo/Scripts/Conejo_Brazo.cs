using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Conejo_Brazo : MonoBehaviour
{

    public GameObject OwnerPlayer;
     
    public Items CurrentItemInHand;

    public enum HandState
    {
        Empty,
        HoldingItem
    }


    public HandState CurrentHandState;

    void Start()
    {
        string name = OwnerPlayer.name;
        CurrentHandState = HandState.Empty;
    }

    public void SetItemInHand(Items item)
    {
        CurrentItemInHand = item;
        
    }

    //Esta funcion adjunta el item en es suelo al brazo del jugador
    public void AttachItemHand(Items item)
    {
        if (item == null) return;

        //Setea el estado y lugar del item en la mano del jugador
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.down;

        //Rotar el item a la rotacion por defecto
        item.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        item.GetComponent<Rigidbody2D>().simulated = false;

        CurrentHandState = HandState.HoldingItem;
        OwnerPlayer.GetComponent<Conejo_CharcterController>().trowAction.action.Enable();

    }

    //Funcion que lanza el item que tiene el jugador en la mano
    public void TrowItem(Items item)
    {
        if (item == null) return;

        // Obtener referencias a los componentes necesarios del item en la mano
        Rigidbody2D itemRB = item.GetComponent<Rigidbody2D>();
        Collider2D itemCol = item.GetComponent<Collider2D>();
        Collider2D playerCol = OwnerPlayer.GetComponent<Collider2D>();

        item.currentState = Items.ItemState.Throwed;
        
        item.transform.SetParent(null);

        itemRB.simulated = true;
        itemRB.bodyType = RigidbodyType2D.Dynamic;
        itemRB.gravityScale = 1f;

        itemCol.isTrigger = false;


        // Calcular la dirección de lanzamiento basada en el ángulo y la dirección del jugador       
        float angleRadians = item.itemTrowAngle * Mathf.Deg2Rad;

        float facingDirection = Mathf.Sign(OwnerPlayer.transform.localScale.x);

        Vector2 throwDirection = new Vector2(Mathf.Cos(angleRadians) * facingDirection, Mathf.Sin(angleRadians));

        float throwRotationZ = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;

        
        // Ajusta la posición de lanzamiento según sea necesario
        item.transform.position = OwnerPlayer.transform.position + (Vector3)(throwDirection.normalized * 3.5f); 

        item.transform.rotation = Quaternion.Euler(0, 0, throwRotationZ);

        

        Physics2D.IgnoreCollision(itemCol, playerCol, true);

        itemRB.linearVelocity = throwDirection * item.itemTrowVelocity;
        
        StartCoroutine(ReenableCollision(itemCol, playerCol, 0.3f));


        CurrentHandState = HandState.Empty;
        OwnerPlayer.GetComponent<Conejo_CharcterController>().trowAction.action.Disable();
    }

    private IEnumerator ReenableCollision(Collider2D itemCol, Collider2D playerCol, float Delay)
    {
        yield return new WaitForSeconds(Delay);

        if (itemCol == null) yield break;
        Physics2D.IgnoreCollision(itemCol, playerCol, false);
    }


    public bool IsHandEmpty()
    {
        return CurrentHandState == HandState.Empty;
    }

   
    void Update()
    {
        
    }
}