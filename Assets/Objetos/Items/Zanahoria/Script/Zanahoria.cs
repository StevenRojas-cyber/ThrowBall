using Unity.VisualScripting;
using UnityEngine;

public class Zanahoria : Items
{
    public BoxCollider2D Hitbox;
    GameObject Player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            Debug.Log("El jugador ha recogido una zanahoria.");
            Hitbox.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        itemObject = this.gameObject;
        itemName = itemObject.name;
       
        Hitbox.enabled = true;

        PrintName();
    }

    private void Update()
    {
   

    }
}
