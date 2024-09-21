using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static ScoreManager Instance { get; private set; }
    public int CurrentScore { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void AddScore(int points)
    {
        CurrentScore += points;
        UpdateScoreTextUI();
    }

    private void UpdateScoreTextUI()
    {
        scoreText.text = "Score: " + CurrentScore.ToString();
    }
}
