using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] answerButtons;

    private Question currentQuestion;

    private void Start()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; 
            answerButtons[i].onClick.AddListener(() => OnAnswerClicked(index));
        }

        LoadNextQuestion();
    }

    private void LoadNextQuestion()
    {
        currentQuestion = GameManager.Instance.GetNextQuestion();
        if (currentQuestion == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Result");
            return;
        }

        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.answers[i];
        }

        int total = GameManager.Instance.GetTotalQuestions();
        int asked = GameManager.Instance.QuestionsAsked;
        progressText.text = $"Вопрос {asked}/{total}";

        foreach (var btn in answerButtons)
        {
            btn.interactable = true;
        }
    }

    private void OnAnswerClicked(int buttonIndex)
    {
        foreach (var btn in answerButtons)
        {
            btn.interactable = false;
        }

        GameManager.Instance.SubmitAnswer(buttonIndex, currentQuestion);

        Invoke(nameof(LoadNextQuestion), 0.5f);
    }
}