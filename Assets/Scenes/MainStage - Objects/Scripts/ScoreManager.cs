using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text conejoScore;
    public TMP_Text zorroScore;
    public TMP_Text WinnerText;

    private ScoreCounter conejoScoreCount;
    private ScoreCounter zorroScoreCount;

    public bool gameEnded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conejoScoreCount = conejoScore.GetComponent<ScoreCounter>();
        zorroScoreCount = zorroScore.GetComponent<ScoreCounter>();

        if (WinnerText == null) return;
        WinnerText.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        if (conejoScoreCount == null || zorroScoreCount == null) return;

        if (conejoScoreCount.scoreCount >= 5)
        {
            EndGame("Conejo");
        }
        else if (zorroScoreCount.scoreCount >= 5)
        {
            EndGame("Zorro");
        }
    }

    void EndGame(string winnerText)
    {
        gameEnded = true;

        WinnerText.text = winnerText + " Gana!";

        WinnerText.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

}