[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;          
    public int correctAnswerIndex;     

    public Question(string text, string[] ans, int correctIdx)
    {
        questionText = text;
        answers = ans;
        correctAnswerIndex = correctIdx;
    }
}