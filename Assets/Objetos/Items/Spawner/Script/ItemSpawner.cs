using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Spawner Settings")]
    public GameObject[] itemPrefab;// Prefab del item a spawnear

    [Header("Spawn Timing Settings")]
    public float minSpawnTime = 2f; // Intervalo de tiempo entre cada spawn
    public float maxSpawnTime = 5f; // Intervalo de tiempo entre cada spawn
    public float maxIncrement = 1f; // Incremento máximo para reducir el tiempo de spawn

    [Header("Emerge Animation Settings")]
    public float emergeHeight = 2f; // Altura a la que el item emerge del suelo
    public float emergeSpeed = 2f; // Velocidad a la que el item emerge del suelo
    public float GroundLenght; // Longitud del suelo para determinar el área de spawn
    public BoxCollider2D groundCollider; // Collider del suelo para determinar el área de spawn

    
    private List<GameObject> ItemsOnGround = new List<GameObject>(); // Lista para rastrear los items spawneados

    private float timer; // Temporizador para controlar el spawn
    



    private void Start()
    {
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
    }


    void SpawnItem()
    {
        // Seleccionamos un prefab aleatorio de la lista de prefabs
        int randomIndex = Random.Range(0, itemPrefab.Length);
        GameObject prefab = itemPrefab[randomIndex];



        //Este bloque se encarga de calcular la posición de spawn del item dentro del área del suelo
        // Obtenemos la longitud del suelo a partir del collider
        float groundLenght = groundCollider.bounds.size.x;

        // Obtenemos la altura del suelo a partir del collider
        float groundHeight = groundCollider.bounds.extents.y;

        // Calculamos una posición aleatoria dentro del área del suelo
        float randomX = Random.Range(groundCollider.bounds.min.x, groundCollider.bounds.max.x);

        //Posición de spawn del item, ajustando la altura para que aparezca justo encima del suelo
        Vector3 spawnPos = new Vector3(randomX, groundCollider.bounds.min.y + groundHeight, 0);




        // Instanciamos el item y lo agregamos a la lista de items en el suelo
        GameObject item = Instantiate(prefab, spawnPos, prefab.name.Contains("Cebolla") ? Quaternion.Euler(0, 0, 90) : Quaternion.Euler(0, 0, -90));
        ItemsOnGround.Add(item);

        Items itemScript = item.GetComponent<Items>();
        itemScript.spawner = this;

        StartCoroutine(EmergeFromGround(item));
    }

    public void NotifyItemCollected(GameObject item)
    {
        ItemsOnGround.Remove(item);
        if(ItemsOnGround.Count == 0)
        {
           StartCoroutine(SpawnDelay());
        }
    }


    IEnumerator SpawnDelay()
    {
        float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(waitTime);

        maxSpawnTime += maxIncrement;

        SpawnItem();
    }

    public void ResetSpawnDelay()
    {
        maxSpawnTime = 5f;
    }


    //Animación de aparición del item emergiendo del suelo
    IEnumerator EmergeFromGround(GameObject item)
    {
        // Obtenemos componentes necesarios
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        CircleCollider2D PickUpTrigger = item.GetComponent<CircleCollider2D>();
        BoxCollider2D bodyCollider = item.GetComponent<BoxCollider2D>();


        // Desactivamos físicas mientras emerge
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        PickUpTrigger.enabled = false;
        bodyCollider.enabled = false;


        // Calculamos posición objetivo
        Vector3 targetPos = item.transform.position + new Vector3(0, emergeHeight, 0);


        // Movemos el item hacia arriba hasta alcanzar la posición objetivo
        while (item != null && Vector3.Distance(item.transform.position, targetPos) > 0.01f)
        {
            item.transform.position = Vector3.MoveTowards(
                item.transform.position,
                targetPos,
                emergeSpeed * Time.deltaTime
            );

            yield return null;
        }

        if(item == null) yield break;



        // Reactivamos físicas
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        PickUpTrigger.enabled = true;
        bodyCollider.enabled = true;
    }



}
