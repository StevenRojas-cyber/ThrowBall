using System.Collections;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Settings")]
    public float spawnHeight;
    public float BegingDelay;
    public float StartImpulseForce;
    public bool isGameEnded = false;

    public TMP_Text conejoScoreText;
    public TMP_Text zorroScoreText;
    public Rigidbody2D BallBody;
    public GameObject ConejoField;
    public GameObject ZorroField;

    Conejo_CharcterController ConejoPlayer;
    Zorro_CharacterController ZorroPlayer;

    private void Start()
    {
        ConejoPlayer = GameObject.FindGameObjectWithTag("Conejo_Player").GetComponent<Conejo_CharcterController>();
        ZorroPlayer = GameObject.FindGameObjectWithTag("Zorro_Player").GetComponent<Zorro_CharacterController>();

        if (ConejoPlayer == null || ZorroPlayer == null)
        {
            Debug.LogError("No se encontraron los jugadores. Asegúrate de que los objetos tengan las etiquetas correctas.");
        }
        else
        { 
            Debug.Log("Jugadores encontrados correctamente.");
        }
    }

    private void Update()
    {
        if(!isGameEnded) return;
        SpawnBall();
    }

    public void GameFinished()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        BallBody.gravityScale = 0;
        transform.position = new Vector3(0, spawnHeight, 0);
        BallBody.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (conejoScoreText == null || zorroScoreText == null) return;

        if (ConejoField == null || ZorroField == null) return;

        ItemSpawner ConejoSpawner = ConejoField.GetComponent<ItemSpawner>();
        ItemSpawner ZorroSpawner = ZorroField.GetComponent<ItemSpawner>();
        

        if (collision.gameObject.CompareTag("Conejo Field"))
        {
            zorroScoreText.GetComponent<ScoreCounter>().AddScore();

            ConejoPlayer.Celebration(false);
            ZorroPlayer.Celebration(true);

            StartCoroutine(PointScored());
        }



        if (collision.gameObject.CompareTag("Zorro Field"))
        {
            conejoScoreText.GetComponent<ScoreCounter>().AddScore();

            ConejoPlayer.Celebration(true);
            ZorroPlayer.Celebration(false);

            StartCoroutine(PointScored());

        }
        
    }

    IEnumerator PointScored()
    { 
        SpawnBall();
        
        yield return new WaitForSeconds(BegingDelay);

        BallBody.gravityScale = 0.2f;
        BallBody.AddForce(new Vector2(Random.Range(-1f, 1f), 0) * StartImpulseForce);

        ConejoField.GetComponent<ItemSpawner>().ResetSpawnDelay();
        ZorroField.GetComponent<ItemSpawner>().ResetSpawnDelay();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (conejoScoreText == null || zorroScoreText == null) return;

        if (ConejoField == null || ZorroField == null) return;

        ItemSpawner ConejoSpawner = ConejoField.GetComponent<ItemSpawner>();
        ItemSpawner ZorroSpawner = ZorroField.GetComponent<ItemSpawner>();

        if (collision.gameObject.CompareTag("Conejo_Player"))
        {
            zorroScoreText.GetComponent<ScoreCounter>().AddScore();
            
            ConejoPlayer.Celebration(false);
            ZorroPlayer.Celebration(true);
            
            StartCoroutine(PointScored());
        }

        if (collision.gameObject.CompareTag("Zorro_Player"))
        {
            conejoScoreText.GetComponent<ScoreCounter>().AddScore();

            ConejoPlayer.Celebration(true);
            ZorroPlayer.Celebration(false);

            StartCoroutine(PointScored());
        }
    }

   
}

