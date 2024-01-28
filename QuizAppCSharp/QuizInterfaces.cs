// QuizInterfaces.cs
using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuizAppCSharp
{
    public interface IQuestionDisplayable
    {
        void DisplayQuestion(QuizBase.QuizQuestion quizQuestion);
    }

    public interface IQuiz
    {
        int CorrectQuestion { get; set; }
        int QNumber { get; set; }
        int Points { get; set; }
        int Percent { get; set; }
        int TotalQuestions { get; set; }

        QuizBase.QuizQuestion AskQuestion(int questionNumber);
    }

    public interface IAnswerable<T>
    {
        bool CheckAnswer(T selectedOption);
    }
}
