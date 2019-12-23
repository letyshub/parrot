using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Bootstrap;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using Letys.Parrot.Properties;
using System.Collections.Generic;

namespace Letys.Parrot.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;
        private Stack<ViewModelBase> previousViewModels;
        private readonly ICategoriesModel categoriesModel;

        #region Properties

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }
            set
            {
                if (this.currentViewModel == value)
                {
                    return;
                }

                this.currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        #endregion

        #region Commands

        private RelayCommand displayCategories;
        public RelayCommand DisplayCategories
        {
            get
            {
                if (this.displayCategories == null)
                {
                    this.displayCategories = new RelayCommand(() =>
                    {
                        this.ClearPreviousViewModels();
                        this.CurrentViewModel = new CategoriesViewModel(this.categoriesModel);
                    });
                }

                return this.displayCategories;
            }
        }

        private RelayCommand exit;
        public RelayCommand Exit
        {
            get
            {
                if (this.exit == null)
                {
                    this.exit = new RelayCommand(() =>
                    {
                        App.Current.Shutdown();
                    });
                }

                return this.exit;
            }
        }

        private RelayCommand displayTests;
        public RelayCommand DisplayTests
        {
            get
            {
                if (this.displayTests == null)
                {
                    this.displayTests = new RelayCommand(() =>
                    {
                        this.ClearPreviousViewModels();
                        this.CurrentViewModel = new TestSetsViewModel();
                    });
                }

                return this.displayTests;
            }
        }

        private RelayCommand displayScores;
        public RelayCommand DisplayScores
        {
            get
            {
                if (this.displayScores == null)
                {
                    this.displayScores = new RelayCommand(() =>
                    {
                        this.ClearPreviousViewModels();
                        this.CurrentViewModel = new ScoresViewModel();
                    });
                }

                return this.displayScores;
            }
        }

        #endregion

        public MainViewModel()
        {
            this.categoriesModel = BootStrapper.Resolve<ICategoriesModel>();
            ApplicationConfiguration.ConnectionString = Settings.Default.ConnectionString;
            this.currentViewModel = new TestSetsViewModel();
            ApplicationMessage.Register<string>(this, MessageType.TestAdd, ReceiveTestAddMessage);
            ApplicationMessage.Register<TestSet>(this, MessageType.TestEdit, ReceiveTestEditMessage);
            ApplicationMessage.Register<string>(this, MessageType.TestsDisplay, ReceiveTestsDisplayMessage);
            ApplicationMessage.Register<TestSet>(this, MessageType.TestRun, ReceiveTestRunMessage);
            ApplicationMessage.Register<TestSet>(this, MessageType.TestLearning, ReceiveTestLearningMessage);
            ApplicationMessage.Register<TestSet>(this, MessageType.TestQuestionsDisplay, ReceiveTestDisplayQuestionsMessage);
            ApplicationMessage.Register<string>(this, MessageType.CloseApp, ReceiveCloseAppMessage);
            ApplicationMessage.Register<string>(this, MessageType.PreviousViewDisplay, ReceiveDisplayPreviousMessage);
            ApplicationMessage.Register<Category>(this, MessageType.CategoryEdit, ReceiveCategoryEditMessage);
            ApplicationMessage.Register<string>(this, MessageType.CategoryAdd, ReceiveCategoryAddMessage);
            ApplicationMessage.Register<string>(this, MessageType.CategoriesDisplay, ReceiveCategoriesDisplayMessage);
        }

        #region Receive Messages

        private void ReceiveCategoryEditMessage(Category category)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new CategoryViewModel(category, this.categoriesModel);
        }

        private void ReceiveCategoryAddMessage(string obj)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new CategoryViewModel(new Category(), this.categoriesModel);
        }

        private void ReceiveCloseAppMessage(string obj)
        {
            App.Current.Shutdown();
        }

        private void ReceiveTestAddMessage(string obj)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new TestSetEditViewModel();
        }

        private void ReceiveTestEditMessage(TestSet test)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new TestSetEditViewModel { SelectedTest = test };
        }

        private void ReceiveTestsDisplayMessage(string obj)
        {
            this.ClearPreviousViewModels();
            this.CurrentViewModel = new TestSetsViewModel();
        }

        private void ReceiveCategoriesDisplayMessage(string obj)
        {
            this.ClearPreviousViewModels();
            this.CurrentViewModel = new CategoriesViewModel(this.categoriesModel);
        }

        private void ReceiveTestRunMessage(TestSet test)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new RunnerTestSetViewModel { Test = test };
        }

        private void ReceiveTestLearningMessage(TestSet test)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new LearningViewModel(test);
        }

        private void ReceiveTestDisplayQuestionsMessage(TestSet test)
        {
            this.AddCurrentViewToPrevious();
            this.CurrentViewModel = new DisplayItemsViewModel(test);
        }

        private void ReceiveDisplayPreviousMessage(string obj)
        {
            var vm = this.previousViewModels.Pop();
            if (vm != null)
            {
                this.CurrentViewModel = vm;
            }
        }

        #endregion

        private void AddCurrentViewToPrevious()
        {
            if (this.previousViewModels == null)
            {
                this.previousViewModels = new Stack<ViewModelBase>();
            }

            this.previousViewModels.Push(this.CurrentViewModel);
        }

        private void ClearPreviousViewModels()
        {
            if (this.previousViewModels != null)
            {
                this.previousViewModels.Clear();
            }
        }
    }
}