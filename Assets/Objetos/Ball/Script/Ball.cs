using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float spawnPoint = 9f;
    public TMP_Text conejoScoreText;
    public TMP_Text zorroScoreText;
    public Rigidbody2D BallBody;

    void SpawnBall()
    {
        transform.position = new Vector3(0, spawnPoint, 0);
        BallBody.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (conejoScoreText == null || zorroScoreText == null) return;

        if (collision.gameObject.CompareTag("Conejo Field"))
        {
            zorroScoreText.GetComponent<ScoreCounter>().AddScore();
            SpawnBall();
        }

        if (collision.gameObject.CompareTag("Zorro Field"))
        {
            conejoScoreText.GetComponent<ScoreCounter>().AddScore();
            SpawnBall();
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (conejoScoreText == null || zorroScoreText == null) return;

        if (collision.gameObject.CompareTag("Conejo_Player"))
        {
            zorroScoreText.GetComponent<ScoreCounter>().AddScore();
            SpawnBall();
        }

        if (collision.gameObject.CompareTag("Zorro_Player"))
        {
            conejoScoreText.GetComponent<ScoreCounter>().AddScore();
            SpawnBall();
        }
    }

}

