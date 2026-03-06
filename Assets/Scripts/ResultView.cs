using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ResultView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI bestResultText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        int score = GameManager.Instance.GetCurrentScore();
        int total = GameManager.Instance.GetTotalQuestions();
        int best = GameManager.Instance.GetBestScore();

        resultText.text = $"Ваш результат: {score}/{total}";
        bestResultText.text = $"Лучший результат: {best}/{total}";

        restartButton.onClick.AddListener(OnRestartClick);
        menuButton.onClick.AddListener(OnMenuClick);
    }

    private void OnRestartClick()
    {
        GameManager.Instance.StartNewQuiz();
    }

    private void OnMenuClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}