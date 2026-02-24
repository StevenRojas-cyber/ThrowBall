using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Spawner Settings")]
    public GameObject[] itemPrefab;// Prefab del item a spawnear
    public float spawnInterval = 5f; // Intervalo de tiempo entre cada spawn
    public int maxItemsInScene = 3; // Cantidad máxima de items en el área de spawn
    public float spawnRadius = 2f;

    private float timer; // Temporizador para controlar el spawn

    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawn();
            timer = 0f;
        }
    }

    void TrySpawn()
    {
        if (GameObject.FindGameObjectsWithTag("Item").Length >= maxItemsInScene)
            return;

        int randomIndex = Random.Range(0, itemPrefab.Length);

        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector2 spawnPosition = (Vector2)transform.position + randomOffset;

        Instantiate(itemPrefab[randomIndex], spawnPosition, Quaternion.identity);
    }

}
