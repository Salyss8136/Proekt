using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<Question> allQuestions;
    private List<Question> unansweredQuestions; 
    private int currentScore = 0;
    private int bestScore = 0;
    private int questionsAsked = 0;

    public int QuestionsAsked => questionsAsked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuestions();
            LoadBestScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeQuestions()
    {
        allQuestions = new List<Question>();

        allQuestions.Add(new Question(
            "Какое животное является самым высоким на планете?",
            new string[] { "Слон", "Жираф", "Бегемот", "Носорог" },
            1)); 

        allQuestions.Add(new Question(
            "Кто из этих животных обычно живёт в доме человека?",
            new string[] { "Лев", "Тигр", "Кошка", "Волк" },
            2)); 

        allQuestions.Add(new Question(
            "Какое животное называют «королём джунглей»?",
            new string[] { "Леопард", "Лев", "Гепард", "Пантера" },
            1)); 

        allQuestions.Add(new Question(
            "Где в природе обитают пингвины?",
            new string[] { "В Арктике", "В Антарктиде", "В Африке", "В Австралии" },
            1)); 

        allQuestions.Add(new Question(
            "Какое животное даёт человеку молоко?",
            new string[] { "Курица", "Корова", "Овца", "Свинья" },
            1));

        allQuestions.Add(new Question(
            "У какого животного есть длинный хобот?",
            new string[] { "Носорог", "Морж", "Слон", "Бегемот" },
            2)); 

        allQuestions.Add(new Question(
            "Кто из этих животных умеет летать?",
            new string[] { "Страус", "Пингвин", "Летучая мышь", "Кенгуру" },
            2));

        allQuestions.Add(new Question(
            "Какое животное может менять цвет своей кожи?",
            new string[] { "Осьминог", "Хамелеон", "Зебра", "Крокодил" },
            1));

        allQuestions.Add(new Question(
            "Кто из этих животных впадает в зимнюю спячку?",
            new string[] { "Заяц", "Лиса", "Медведь", "Волк" },
            2)); 

        allQuestions.Add(new Question(
            "Какое животное считается самым быстрым на суше?",
            new string[] { "Лев", "Антилопа", "Гепард", "Страус" },
            2)); 
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    public void StartNewQuiz()
    {
        currentScore = 0;
        questionsAsked = 0;
        unansweredQuestions = new List<Question>(allQuestions);
        Shuffle(unansweredQuestions);
        SceneManager.LoadScene("Quiz");
    }

    public Question GetNextQuestion()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
            return null;

        Question nextQ = unansweredQuestions[0];
        unansweredQuestions.RemoveAt(0);
        questionsAsked++;
        return nextQ;
    }

    public void SubmitAnswer(int selectedAnswerIndex, Question question)
    {
        if (selectedAnswerIndex == question.correctAnswerIndex)
        {
            currentScore++;
        }

        if (unansweredQuestions.Count == 0)
        {
            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                SaveBestScore();
            }
            SceneManager.LoadScene("Result");
        }
    }

    public int GetCurrentScore() => currentScore;
    public int GetBestScore() => bestScore;
    public int GetTotalQuestions() => allQuestions.Count;

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}