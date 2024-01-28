using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizAppCSharp
{

    //clasa abstracta care implementeaza interfata IQuiz
    //CLASA ABSTRACTA
    public abstract class QuizBase : IQuiz
    {
        //proprietati
        public int CorrectQuestion { get; set; }
        public int QNumber { get; set; } = 1;
        public int Points { get; set; }
        public int Percent { get; set; }
        public int TotalQuestions { get; set; }

        
        //lista de obiecte de tip QuizQuestion(lista intrebariilor)
        //COLECTII
        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();

        
        //constructor ce initalizeaza intrebarile
        public QuizBase()
        {
            TotalQuestions = 4;
            InitializeQuestions();
        }

        //clasa abstracta ce primeste numarul de intrebari si returneaza un obiect de tip QuizQuestion
        public abstract QuizQuestion AskQuestion(int questionNumber);

        //pentru a initializa lista de intrebari
        protected abstract void InitializeQuestions();

        //clasa ce reprezinta o intrebare(propietati: imagine, intrebare, optiuni, raspuns corect)
        //GENERICITATE
        public class QuizQuestion : IAnswerable<int>
        {
            public Image Image { get; set; }
            public string Question { get; set; }
            public List<string> Options { get; set; }
            public int CorrectOption { get; set; }

            //pentru a verifica raspunsul intrebarii
            public bool CheckAnswer(int selectedOption)
            {
                return selectedOption == CorrectOption;
            }
        }

        //referire la obiectul Form1
        public Form1 Owner { get; internal set; }
    }
}
