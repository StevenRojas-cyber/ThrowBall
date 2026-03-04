using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Spawner Settings")]
    public GameObject[] itemPrefab;// Prefab del item a spawnear
    public float spawnInterval = 5f; // Intervalo de tiempo entre cada spawn
    public float emergeHeight = 2f;
    public float emergeSpeed = 2f;
    public float GroundLenght; 

    private float timer; // Temporizador para controlar el spawn


    private void Start()
    {
        InvokeRepeating(nameof(SpawnItem), 1.5f, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnItem()
    {
        if (itemPrefab.Length == 0) return;
        int randomIndex = Random.Range(0, itemPrefab.Length);
        GameObject itemToSpawn = itemPrefab[randomIndex];

        BoxCollider2D groundCollider = GetComponent<BoxCollider2D>();
        if (groundCollider == null) return;
        GroundLenght = groundCollider.size.x;

        float randomX = Random.Range(-GroundLenght / 2f, GroundLenght / 2f);

        Vector3 spawnPos = transform.position;

        // Instanciamos un poco debajo del suelo
        Vector3 startPos = spawnPos - new Vector3(randomX, emergeHeight, 0);

        GameObject newItem = Instantiate(itemToSpawn, startPos, Quaternion.identity);
        
        StartCoroutine(EmergeFromGround(newItem));
    }

    
    IEnumerator EmergeFromGround(GameObject item)
    {
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();

        // Desactivamos físicas mientras emerge
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;

        Vector3 targetPos = item.transform.position + new Vector3(0, emergeHeight, 0);

        while (Vector3.Distance(item.transform.position, targetPos) > 0.01f)
        {
            item.transform.position = Vector3.MoveTowards(
                item.transform.position,
                targetPos,
                emergeSpeed * Time.deltaTime
            );

            yield return null;
        }

        // Reactivamos físicas
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }

}
