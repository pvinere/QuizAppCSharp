// QuizInterfaces.cs
using System;
using System.Collections.Generic;
using System.Drawing;

namespace QuizAppCSharp
{

    //interfata ce defineste metoda de afisare a unei intrebari
    //INTERFETE
    public interface IQuestionDisplayable
    {
        void DisplayQuestion(QuizBase.QuizQuestion quizQuestion);
    }

    //interfata ce defineste metoda AskQuestion si cateva proprietati
    public interface IQuiz
    {
        int CorrectQuestion { get; set; }
        int QNumber { get; set; }
        int Points { get; set; }
        int Percent { get; set; }
        int TotalQuestions { get; set; }

        QuizBase.QuizQuestion AskQuestion(int questionNumber);
    }

    //interfata generica pentru a verifica raspunsul
    //GENERICITATE
    public interface IAnswerable<T>
    {
        bool CheckAnswer(T selectedOption);
    }
}
