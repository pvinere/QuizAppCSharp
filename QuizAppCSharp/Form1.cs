using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuizAppCSharp.QuizBase;


namespace QuizAppCSharp
{
    public interface IQuestionDisplayable
    {
        void DisplayQuestion(QuizQuestion quizQuestion);
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


    public abstract class QuizBase : IQuiz
    {
        // Proprietati pentru variabilele existente
        public int CorrectQuestion { get; set; }
        public int QNumber { get; set; } = 1;
        public int Points { get; set; }
        public int Percent { get; set; }
        public int TotalQuestions { get; set; }

        //colectie
        public List<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();

        // Constructor implicit
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


        public class QuizManager : QuizBase, IQuestionDisplayable
        {

            public void DisplayQuestion(QuizQuestion quizQuestion)
            {
                // Call the method in the Form1 class to update the UI
                ((IQuestionDisplayable)this.Owner).DisplayQuestion(quizQuestion);
            }


            public override QuizQuestion AskQuestion(int questionNumber)
            {

                int index = questionNumber - 1;

                if (index >= 0 && index < Questions.Count)
                {
                    return Questions[index];
                }
                else
                {
                    // Returnăm null dacă numărul întrebării nu este valid
                    return null;
                }
            }


            protected override void InitializeQuestions()
            {
                // Adaugăm întrebările în colecție
                Questions.Add(new QuizQuestion
                {
                    Image = Properties.Resources.romania_flag,
                    Question = "Care țară are acest steag?",
                    Options = new List<string> { "Germania", "Franța", "Italia", "România" },
                    CorrectOption = 4
                });

                Questions.Add(new QuizQuestion
                {
                    Image = Properties.Resources.klaus,
                    Question = "Care e președintele României?",
                    Options = new List<string> { "Joe Biden", "Klaus Iohannis", "Emmanuel Macron", "Andrzej Duda" },
                    CorrectOption = 2
                });

                Questions.Add(new QuizQuestion
                {
                    Image = Properties.Resources.romania_map_grey,
                    Question = "Cu câte țări se învecinează România?",
                    Options = new List<string> { "4", "5", "3", "6" },
                    CorrectOption = 2
                });

                Questions.Add(new QuizQuestion
                {
                    Image = Properties.Resources.ro_eu_flags,
                    Question = "În ce an a intrat România în Uniunea Europeană?",
                    Options = new List<string> { "1999", "2007", "2010", "2005" },
                    CorrectOption = 2
                });
            }

        }

        public Form1 Owner { get; internal set; }
    }



    public partial class Form1 : Form, IQuestionDisplayable
    {
        public void DisplayQuestion(QuizQuestion quizQuestion)
        {
            pictureBox1.Image = quizQuestion.Image;
            labelQuestion.Text = quizQuestion.Question;
            button1.Text = quizQuestion.Options[0];
            button2.Text = quizQuestion.Options[1];
            button3.Text = quizQuestion.Options[2];
            button4.Text = quizQuestion.Options[3];
        }

        private QuizBase quizManager;
        private QuizQuestion currentQuestion;

        // Constructor implicit
        public Form1()
        {
            InitializeComponent();
            quizManager = new QuizBase.QuizManager();
            quizManager.Owner = this;

            // Asigură-te că AskQuestion întoarce o instanță validă a QuizQuestion
            currentQuestion = quizManager.AskQuestion(quizManager.QNumber);

            // Verifică dacă currentQuestion este diferit de null înainte de a-l utiliza
            if (currentQuestion != null)
            {
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Nu s-au putut încărca datele pentru întrebare.");
            }


        }



        private void checkAnswerEvent(object sender, EventArgs e)
        {
            var buttonObj = (Button)sender;

            int buttonTag = Convert.ToInt32(buttonObj.Tag);

            if (buttonTag == currentQuestion.CorrectOption)
            {
                quizManager.Points++;
            }

            if (quizManager.QNumber == quizManager.TotalQuestions)
            {


                MessageBox.Show(
                  "The End" + Environment.NewLine +
                  "Correct answers: " + quizManager.Points + "!" + Environment.NewLine +
                  "Press OK to restart the quiz!");

                SaveResultsToFile();

                quizManager.Points = 0;
                quizManager.QNumber = 0;
                currentQuestion = quizManager.AskQuestion(quizManager.QNumber);
            }


            quizManager.QNumber++;
            currentQuestion = quizManager.AskQuestion(quizManager.QNumber);
            UpdateUI();
        }

        private void UpdateUI()
        {
            Action updateAction = () =>
            {
                pictureBox1.Image = currentQuestion.Image;
                labelQuestion.Text = currentQuestion.Question;
                button1.Text = currentQuestion.Options[0];
                button2.Text = currentQuestion.Options[1];
                button3.Text = currentQuestion.Options[2];
                button4.Text = currentQuestion.Options[3];
            };

            if (InvokeRequired)
            {
                Invoke(updateAction);
            }
            else
            {
                updateAction();
            }
        }

        public class SaveResultsToFileException : Exception
        {
            public SaveResultsToFileException(string message) : base(message)
            {
            }
        }

        private void SaveResultsToFile()
        {
            string fileName = "QuizResults.txt";

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
                {
                    file.WriteLine("Quiz Results:");
                    file.WriteLine($"Correct Answers: {quizManager.Points}");
                    file.WriteLine($"Total Questions: {quizManager.TotalQuestions}");
                    file.WriteLine($"Percentage: {((double)quizManager.Points / quizManager.TotalQuestions) * 100}%");
                    file.WriteLine(new string('-', 30));
                }
            }
            catch (Exception ex)
            {
                throw new SaveResultsToFileException($"Error saving results to file: {ex.Message}");
            }
        }




    }
}