using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Bootstrap;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using Letys.Parrot.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Letys.Parrot.ViewModel
{
    public class TestSetsViewModel : ViewModelBase
    {
        private ObservableCollection<TestSet> tests;
        private ObservableCollection<Category> categories;
        private readonly ITestSetsModel testSetsModel;
        private readonly ICategoriesModel categoriesModel;
        private TestSet selectedTest;
        private Category selectedCategory;

        public TestSetsViewModel()
        {
            try
            {
                this.testSetsModel = BootStrapper.Resolve<ITestSetsModel>();
                this.categoriesModel = BootStrapper.Resolve<ICategoriesModel>();
                this.tests = this.testSetsModel.GetAll();
                this.categories = this.categoriesModel.GetAll();
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        #region Command

        private RelayCommand add;
        public RelayCommand Add
        {
            get
            {
                if (this.add == null)
                {
                    this.CreateAddCommand();
                }

                return this.add;
            }
        }

        private RelayCommand<TestSet> edit;
        public RelayCommand<TestSet> Edit
        {
            get
            {
                if (this.edit == null)
                {
                    this.CreateEditCommand();
                }

                return this.edit;
            }
        }

        private RelayCommand<TestSet> delete;
        public RelayCommand<TestSet> Delete
        {
            get
            {
                if (this.delete == null)
                {
                    this.CreateDeleteCommand();
                }

                return this.delete;
            }
        }

        private RelayCommand<TestSet> run;
        public RelayCommand<TestSet> Run
        {
            get
            {
                if (this.run == null)
                {
                    this.CreateRunCommand();
                }

                return this.run;
            }
        }

        private RelayCommand<TestSet> displayQuestions;
        public RelayCommand<TestSet> DisplayQuestions
        {
            get
            {
                if (this.displayQuestions == null)
                {
                    this.CreateDisplayQuestionsCommand();
                }

                return this.displayQuestions;
            }
        }

        private RelayCommand<TestSet> learning;
        public RelayCommand<TestSet> Learning
        {
            get
            {
                if (this.learning == null)
                {
                    this.CreateLearningCommand();
                }

                return this.learning;
            }
        }

        private RelayCommand filterByCategory;
        public RelayCommand FilterByCategory
        {
            get
            {
                if (this.filterByCategory == null)
                {
                    this.CreateFilterByCategoryCommand();
                }

                return this.filterByCategory;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<TestSet> Tests
        {
            get
            {
                return this.tests;
            }
        }

        public TestSet SelectedTest
        {
            get
            {
                return this.selectedTest;
            }

            set
            {
                this.selectedTest = value;
                base.RaisePropertyChanged("SelectedTest");
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }

            set
            {
                this.selectedCategory = value;

                base.RaisePropertyChanged("SelectedCategory");
            }
        }

        #endregion

        #region Private methods

        private void CreateFilterByCategoryCommand()
        {
            this.filterByCategory = new RelayCommand(() =>
            {
                try
                {
                    if (this.selectedCategory == null)
                    {
                        return;
                    }

                    this.tests = new ObservableCollection<TestSet>();

                    foreach (TestSet test in this.testSetsModel.GetAll().Where(x => x.Category.Id == this.selectedCategory.Id))
                    {
                        this.tests.Add(test);
                    }
                    base.RaisePropertyChanged("Tests");
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateLearningCommand()
        {
            this.learning = new RelayCommand<TestSet>(test =>
            {
                try
                {
                    ApplicationMessage.Send(MessageType.TestLearning, test);
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateDisplayQuestionsCommand()
        {
            this.displayQuestions = new RelayCommand<TestSet>(test =>
            {
                try
                {
                    ApplicationMessage.Send(MessageType.TestQuestionsDisplay, test);
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateAddCommand()
        {
            try
            {
                this.add = new RelayCommand(() =>
                {
                    ApplicationMessage.Send<string>(Messages.Enum.MessageType.TestAdd, "Add test");
                });
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        private void CreateEditCommand()
        {
            try
            {
                this.edit = new RelayCommand<TestSet>(test =>
                {
                    ApplicationMessage.Send(MessageType.TestEdit, test);
                });
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        private void CreateDeleteCommand()
        {
            this.delete = new RelayCommand<TestSet>(test =>
            {
                try
                {
                    DeleteConfirmWindow win = new DeleteConfirmWindow();
                    if (win.ShowDialog() ?? false)
                    {
                        this.testSetsModel.Delete(test);
                        this.tests = this.testSetsModel.GetAll();
                        base.RaisePropertyChanged("Tests");
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateRunCommand()
        {
            this.run = new RelayCommand<TestSet>(test =>
            {
                try
                {
                    ApplicationMessage.Send(MessageType.TestRun, test);
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        #endregion
    }
}
