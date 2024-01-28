using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppCSharp
{
    public class QuizManager : QuizBase, IQuestionDisplayable
    {

        public void DisplayQuestion(QuizQuestion quizQuestion)
        {
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
                
                return null;
            }
        }

        protected override void InitializeQuestions()
        {
           
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
}
