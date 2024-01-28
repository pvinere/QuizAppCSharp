using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizAppCSharp
{
    public abstract class QuizBase : IQuiz
    {
        
        public int CorrectQuestion { get; set; }
        public int QNumber { get; set; } = 1;
        public int Points { get; set; }
        public int Percent { get; set; }
        public int TotalQuestions { get; set; }

        
        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();

        
        public QuizBase()
        {
            TotalQuestions = 4;
            InitializeQuestions();
        }

        public abstract QuizQuestion AskQuestion(int questionNumber);

        protected abstract void InitializeQuestions();

        public class QuizQuestion : IAnswerable<int>
        {
            public Image Image { get; set; }
            public string Question { get; set; }
            public List<string> Options { get; set; }
            public int CorrectOption { get; set; }

            public bool CheckAnswer(int selectedOption)
            {
                return selectedOption == CorrectOption;
            }
        }

        public Form1 Owner { get; internal set; }
    }
}
