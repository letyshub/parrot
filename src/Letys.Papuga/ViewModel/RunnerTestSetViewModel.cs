using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Bootstrap;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using Letys.Parrot.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Letys.Parrot.ViewModel
{
    public class RunnerTestSetViewModel : ApplicationViewModel
    {
        #region Fields

        private TestSet test;
        private ObservableCollection<TestItem> items;
        private int questionNumber;
        private decimal score;
        private string userAnswer;
        private IList<TestItem> wrongAnswers;
        private ITestSetsModel testSetsModel;
        private IScoreModel scoreModel;

        #endregion

        public RunnerTestSetViewModel()
        {
            try
            {
                this.wrongAnswers = new List<TestItem>();
                this.testSetsModel = BootStrapper.Resolve<ITestSetsModel>();
                this.scoreModel = BootStrapper.Resolve<IScoreModel>();
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        #region Properties

        public int QuestionsQuantity
        {
            get
            {
                return this.items.Count;
            }
        }

        public int QuestionNumber
        {
            get
            {
                return this.questionNumber + 1;
            }
        }

        public string Score
        {
            get
            {
                if (this.questionNumber == 0)
                {
                    return "0,00 %";
                }

                this.score = (this.questionNumber - this.wrongAnswers.Count) / (decimal)this.questionNumber;
                return string.Format("{0:0.00} %", this.score * 100);
            }
        }

        public TestSet Test
        {
            get
            {
                return this.test;
            }

            set
            {
                this.test = value;
                this.items = new ObservableCollection<TestItem>();

                foreach (TestItem item in test.Items)
                {
                    this.items.Add(item);
                }

                base.RaisePropertyChanged("Test");
            }
        }

        public string TestLastScore
        {
            get
            {
                return this.test.LastResultText;
            }
        }

        public string TestName
        {
            get
            {
                return this.test.Name;
            }
        }

        public Category TestCategory
        {
            get
            {
                return this.test.Category;
            }
        }

        public TestLanguage TestLanguage
        {
            get
            {
                return this.test.Language;
            }
        }

        public string TestDescription
        {
            get
            {
                return this.test.Description;
            }
        }

        public string Question
        {
            get
            {
                return this.items[this.questionNumber].Question;
            }
        }

        public string Answer
        {
            get
            {
                return this.userAnswer;
            }

            set
            {
                this.userAnswer = value;
            }
        }

        #endregion

        #region Commands

        private RelayCommand checkAnswer;
        public RelayCommand CheckAnswer
        {
            get
            {
                if (this.checkAnswer == null)
                {
                    this.CreateCheckAnswerCommand();
                }

                return this.checkAnswer;
            }
        }

        #endregion

        private void CreateCheckAnswerCommand()
        {
            this.checkAnswer = new RelayCommand(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(this.userAnswer))
                    {
                        return;
                    }

                    if (this.userAnswer.ToUpper() != this.items[this.questionNumber].Answer.ToUpper())
                    {
                        this.wrongAnswers.Add(this.items[this.questionNumber]);
                        CorrectAnswerWindow win = new CorrectAnswerWindow(this.items[this.questionNumber]);
                        win.ShowDialog();
                    }

                    this.questionNumber++;
                    base.RaisePropertyChanged("Score");

                    if (this.questionNumber == this.items.Count)
                    {
                        WrongAnswersWindow win = new WrongAnswersWindow(this.wrongAnswers);
                        win.ShowDialog();
                        decimal score = (this.questionNumber - this.wrongAnswers.Count) / (decimal)this.questionNumber;
                        this.scoreModel.SaveScore(new Score() { ScoreValue = score, TestHeaderId = this.test.Id });

                        ApplicationMessage.Send(MessageType.TestsDisplay, "Display tests");

                        return;
                    }

                    base.RaisePropertyChanged("Question");
                    this.userAnswer = string.Empty;
                    base.RaisePropertyChanged("Answer");
                    base.RaisePropertyChanged("QuestionNumber");
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }
    }
}
