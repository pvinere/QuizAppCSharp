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
    public partial class Form1 : Form, IQuestionDisplayable
    {
        private QuizBase quizManager;
        private QuizQuestion currentQuestion;

        public void DisplayQuestion(QuizQuestion quizQuestion)
        {
            pictureBox1.Image = quizQuestion.Image;
            labelQuestion.Text = quizQuestion.Question;
            button1.Text = quizQuestion.Options[0];
            button2.Text = quizQuestion.Options[1];
            button3.Text = quizQuestion.Options[2];
            button4.Text = quizQuestion.Options[3];
        }

        
        
        public Form1()
        {
            InitializeComponent();
            quizManager = new QuizManager();
            quizManager.Owner = this;

           
            currentQuestion = quizManager.AskQuestion(quizManager.QNumber);

            
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