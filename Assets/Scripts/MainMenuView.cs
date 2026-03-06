using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClick);
        leaderboardButton.onClick.AddListener(OnLeaderboardClick);
        exitButton.onClick.AddListener(OnExitClick);
    }

    private void OnStartClick()
    {
        GameManager.Instance.StartNewQuiz();
    }

    private void OnLeaderboardClick()
    {
        int best = GameManager.Instance.GetBestScore();
        Debug.Log($"Лучший результат: {best}/{GameManager.Instance.GetTotalQuestions()}");
    }

    private void OnExitClick()
    {
        Application.Quit();
    }
}