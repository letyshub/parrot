using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Core;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Letys.Parrot.ViewModel
{
    public class LearningViewModel : ApplicationViewModel
    {
        private string question = string.Empty;
        private string answer = string.Empty;
        private string example = string.Empty;
        private TestSet testHeader;
        private int questionNo;
        private bool showFrontSide;

        public LearningViewModel(TestSet testHeader)
        {
            this.testHeader = testHeader;
            this.questionNo = 0;
            this.showFrontSide = true;
            this.DisplayFlashcards();
        }


        #region Properties

        public TestSet TestHeader
        {
            get
            {
                return this.testHeader;
            }
        }

        public int QuestionNumber
        {
            get
            {
                return this.questionNo + 1;
            }
        }

        public Visibility ShowFrontSide
        {
            get
            {
                return this.showFrontSide ? Visibility .Visible : Visibility.Collapsed;
            }
        }

        public Visibility ShowBackSide
        {
            get
            {
                return this.showFrontSide ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public string Question
        {
            get
            {
                return this.question;
            }

            set
            {
                this.question = value;
                base.RaisePropertyChanged("Question");
            }
        }

        public string Answer
        {
            get
            {
                return this.answer;
            }

            set
            {
                this.answer = value;
                base.RaisePropertyChanged("Answer");
            }
        }

        public string Example
        {
            get
            {
                return this.example;
            }

            set
            {
                this.example = value;
                base.RaisePropertyChanged("Example");
            }
        }

        #endregion

        #region Commands

        private RelayCommand displayPreviousCommand;
        public RelayCommand DisplayPreviousCommand
        {
            get
            {
                if (this.displayPreviousCommand == null)
                {
                    this.CreateDisplayPreviousCommand();
                }

                return this.displayPreviousCommand;
            }
        }

        private RelayCommand displayNextCommand;
        public RelayCommand DisplayNextCommand
        {
            get
            {
                if (this.displayNextCommand == null)
                {
                    this.CreateDisplayNextCommand();
                }

                return this.displayNextCommand;
            }
        }

        private RelayCommand flipCommand;
        public RelayCommand FlipCommand
        {
            get
            {
                if (this.flipCommand == null)
                {
                    this.CreateFlipCommand();
                }

                return this.flipCommand;
            }
        }

        #endregion

        #region Private Methods

        private void CreateFlipCommand()
        {
            this.flipCommand = new RelayCommand(() =>
            {
                this.showFrontSide = !this.showFrontSide;
                this.RaiseNavigationProperties();
            });
        }

        private void RaiseNavigationProperties()
        {
            base.RaisePropertyChanged("ShowFrontSide");
            base.RaisePropertyChanged("ShowBackSide");
            base.RaisePropertyChanged("QuestionNumber");
        }

        private void CreateDisplayPreviousCommand()
        {
            this.displayPreviousCommand = new RelayCommand(() =>
            {
                try
                {
                    if (this.questionNo > 0)
                    {
                        this.questionNo--;
                        this.RaiseNavigationProperties();
                        this.DisplayFlashcards();
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateDisplayNextCommand()
        {
            this.displayNextCommand = new RelayCommand(() =>
            {
                try
                {
                    if (this.questionNo < this.testHeader.Items.Count)
                    {
                        this.questionNo++;
                        this.RaiseNavigationProperties();
                        this.DisplayFlashcards();
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void DisplayFlashcards()
        {
            this.Question = this.testHeader.Items[this.questionNo].Question;
            this.Answer = this.testHeader.Items[this.questionNo].Answer;
            this.Example = this.testHeader.Items[this.questionNo].Example;
        }

        #endregion
    }
}
