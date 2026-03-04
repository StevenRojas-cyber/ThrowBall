using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    public TMP_Text ScoreText;
    public float scoreCount = 0f;

    public void AddScore()
    {
        if (FindAnyObjectByType<ScoreManager>().gameEnded) return;

        scoreCount += 1f;
        ScoreText.text = scoreCount.ToString();
    }

}
