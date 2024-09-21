using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float DefaultGamePlayDuration { get; set; }
    private float elaspedTime;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseMenu;

    private int highScore;
    private int currentScore;

    public bool isGameOver;
    public bool isStarted;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void GameOver()
    {
        currentScore = ScoreManager.Instance.CurrentScore;

        Time.timeScale = 0;
        isGameOver = true;
        isStarted = false;
        gameplayPanel.SetActive(false);
        if (currentScore > highScore)
        {
            highScore = currentScore;
            gameOverText.text = "GameOver\nScore: " + currentScore.ToString() + "\nHighscore: " + highScore.ToString();
            // Save Highscore
            DataManager.Instance.SaveToJson(highScore);
        }
        else
        {
            gameOverText.text = "GameOver\nScore: " + currentScore.ToString() + " \n Your Highscore: " + highScore.ToString();
        }
        gameOverPanel.SetActive(true);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isStarted = true;
        DefaultGamePlayDuration = 15.0f;
        elaspedTime = DefaultGamePlayDuration;
        highScore = DataManager.Instance.LoadJson().highscore;
        Time.timeScale = 1;
    }
    public void ResetOnRestart()
    {
        elaspedTime = DefaultGamePlayDuration;
        isGameOver = false;
        isStarted = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false && isStarted)
        {
            elaspedTime -= Time.deltaTime;

            if (elaspedTime <= 0.0f)
            {
                GameOver();
            }

            int minutes = (int)elaspedTime / 60;
            timerText.text = "Timer: " + string.Format("{0:D2}:{1:D2}", minutes, (int)elaspedTime);
        }
    }

    public void IncreaseGameTime(float amount)
    {
        elaspedTime += amount;
    }

    public void Pause()
    {
        gameplayPanel.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        gameplayPanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        ResetOnRestart();
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


}
