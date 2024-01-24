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
    public interface IQuiz
    {
        int CorrectQuestion { get; set; }
        int QNumber { get; set; }
        int Points { get; set; }
        int Percent { get; set; }
        int TotalQuestions { get; set; }

        QuizBase.QuizQuestion AskQuestion(int questionNumber);
    }

    public interface IAnswerable
    {
        bool CheckAnswer(int selectedOption);
    }


    public abstract class QuizBase : IQuiz
    {
        // Proprietati pentru variabilele existente
        public int CorrectQuestion { get; set; }
        public int QNumber { get; set; } = 1;
        public int Points { get; set; }
        public int Percent { get; set; }
        public int TotalQuestions { get; set; }
        
        // Constructor implicit
        public QuizBase()
        {
            TotalQuestions = 4;
        }

        public abstract QuizQuestion AskQuestion(int questionNumber);
       

        public class QuizQuestion : IAnswerable
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


        public class QuizManager: QuizBase
        {


            public override QuizQuestion AskQuestion(int questionNumber)
            {

                QuizQuestion quizQuestion = null;

                switch (questionNumber)
                {
                    case 1:
                        quizQuestion = new QuizQuestion
                        {
                            Image = Properties.Resources.romania_flag,
                            Question = "Care tara are acest steag?",
                            Options = new List<string> { "Germania", "Franta", "Italia", "Romania" },
                            CorrectOption = 4
                        };

                        break;

                    case 2:
                        quizQuestion = new QuizQuestion
                        {
                            Image = Properties.Resources.klaus,
                            Question = "Care e presedintele Romaniei?",
                            Options = new List<string> { "Joe Biden", "Klaus Iohannis", "Emmanuel Macron", "Andrzej Duda" },
                            CorrectOption = 2
                        };

                        break;

                    case 3:
                        quizQuestion = new QuizQuestion
                        {
                            Image = Properties.Resources.romania_map_grey,
                            Question = "Cu cate tari se invecineaza Romania?",
                            Options = new List<string> { "4", "5", "3", "6" },
                            CorrectOption = 2
                        };

                        break;

                    case 4:
                        quizQuestion = new QuizQuestion
                        {
                            Image = Properties.Resources.ro_eu_flags,
                            Question = "In ce an a intrat Romania in Uniunea Europeana?",
                            Options = new List<string> { "1999", "2007", "2010", "2005" },
                            CorrectOption = 2
                        };

                        break;
                }

                return quizQuestion;

            }

        }
    }

        

    public partial class Form1 : Form
    {

        private QuizBase quizManager;
        private QuizQuestion currentQuestion;

        // Constructor implicit
        public Form1()
        {
            InitializeComponent();
            quizManager = new QuizBase.QuizManager();

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

            if(buttonTag == currentQuestion.CorrectOption)
            {
                quizManager.Points++;
            }

            if(quizManager.QNumber == quizManager.TotalQuestions)
            {
                

                MessageBox.Show(
                  "The End" + Environment.NewLine +
                  "Correct answers: " + quizManager.Points + "!" + Environment.NewLine +
                  "Press OK to restart the quiz!");

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
            pictureBox1.Image = currentQuestion.Image;
            labelQuestion.Text = currentQuestion.Question;
            button1.Text = currentQuestion.Options[0];
            button2.Text = currentQuestion.Options[1];
            button3.Text = currentQuestion.Options[2];
            button4.Text = currentQuestion.Options[3];
        }

        
    }
}
