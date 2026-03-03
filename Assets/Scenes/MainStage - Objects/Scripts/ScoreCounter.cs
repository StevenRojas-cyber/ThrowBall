using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TMP_Text ScoreText;
    public float scoreCount = 0f;

    public void AddScore()
    {
       scoreCount += 1f;
       ScoreText.text = scoreCount.ToString();
    }
}
