using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Bootstrap;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using Letys.Parrot.Views;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Letys.Parrot.ViewModel
{
    public class TestSetEditViewModel : ApplicationViewModel
    {
        private TestItem selectedItem;
        private ObservableCollection<TestItem> items;
        private TestSet selectedTest;
        private ObservableCollection<Category> categories;
        private ObservableCollection<TestLanguage> languages;
        private ITestSetsModel testSetsModel;
        private ICategoriesModel categoriesModel;

        public TestSetEditViewModel()
        {
            try
            {
                this.selectedTest = new TestSet();
                this.testSetsModel = BootStrapper.Resolve<ITestSetsModel>();
                this.categoriesModel = BootStrapper.Resolve<ICategoriesModel>();

                this.items = new ObservableCollection<TestItem>();
                if (this.selectedTest.Items != null)
                {
                    foreach (TestItem item in this.selectedTest.Items)
                    {
                        this.items.Add(item);
                    }
                }
                this.categories = this.categoriesModel.GetAll();
                this.languages = this.testSetsModel.GetAllLanguages();
                this.SelectedLanguage = TestLanguage.English;
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        #region Properties

        public TestSet SelectedTest
        {
            get
            {
                return this.selectedTest;
            }

            set
            {
                this.selectedTest = value;
                this.items = new ObservableCollection<TestItem>();
                foreach (TestItem item in this.selectedTest.Items)
                {
                    this.items.Add(item);
                }

                base.RaisePropertyChanged("SelectedTest");
            }
        }

        public string TestName
        {
            get
            {
                return this.selectedTest.Name;
            }

            set
            {
                this.selectedTest.Name = value;
                base.RaisePropertyChanged("TestName");
            }
        }

        public string TestDescription
        {
            get
            {
                return this.selectedTest.Description;
            }

            set
            {
                this.selectedTest.Description = value;
                base.RaisePropertyChanged("TestDescription");
            }
        }

        public Category SelectedCategory
        {
            get
            {
                if (this.selectedTest == null || this.selectedTest.Category == null)
                {
                    return null;
                }

                return this.categories.Where(x => x.Id == this.selectedTest.Category.Id).FirstOrDefault();
            }

            set
            {
                this.selectedTest.Category = value;
                base.RaisePropertyChanged("SelectedCategory");
            }
        }

        public TestLanguage SelectedLanguage
        {
            get
            {
                return this.selectedTest.Language;
            }

            set
            {
                this.selectedTest.Language = value;
                base.RaisePropertyChanged("SelectedLanguage");
            }
        }

        public ObservableCollection<TestItem> Questions
        {
            get
            {
                return this.items;
            }
        }

        public int QuestionQuantity
        {
            get
            {
                return this.items.Count;
            }
        }

        public ObservableCollection<TestLanguage> Languages
        {
            get
            {
                return this.languages;
            }
        }

        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }
        }

        public TestItem SelectedQuestion
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
                base.RaisePropertyChanged("SelectedQuestion");
                base.RaisePropertyChanged("QuestionEditDeleteEnabled");
            }
        }

        public bool QuestionEditDeleteEnabled
        {
            get
            {
                return this.selectedItem != null;
            }
        }

        #endregion

        #region Commands

        private RelayCommand questionAdd;
        public RelayCommand QuestionAdd
        {
            get
            {
                if (this.questionAdd == null)
                {
                    this.CreateQuestionAddCommand();
                }

                return this.questionAdd;
            }
        }

        private RelayCommand importCommand;
        public RelayCommand ImportCommand
        {
            get
            {
                if (this.importCommand == null)
                {
                    this.CreateImportCommand();
                }

                return this.importCommand;
            }
        }

        private RelayCommand<TestItem> questionEdit;
        public RelayCommand<TestItem> QuestionEdit
        {
            get
            {
                if (this.questionEdit == null)
                {
                    this.CreateQuestionEditCommand();
                }

                return this.questionEdit;
            }
        }

        private RelayCommand<TestItem> questionDelete;
        public RelayCommand<TestItem> QuestionDelete
        {
            get
            {
                if (this.questionDelete == null)
                {
                    this.CreateQuestionDeleteCommand();
                }

                return this.questionDelete;
            }
        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                if (this.save == null)
                {
                    this.CreateSaveCommand();
                }

                return this.save;
            }
        }

        private RelayCommand cancel;
        public RelayCommand Cancel
        {
            get
            {
                if (this.cancel == null)
                {
                    this.CreateCancelCommand();
                }

                return this.cancel;
            }
        }

        #endregion

        #region Private methods

        private void CreateCancelCommand()
        {
            this.cancel = new RelayCommand(() =>
            {
                try
                {
                    ApplicationMessage.Send(MessageType.TestsDisplay, MessageType.TestsDisplay.ToString());
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateImportCommand()
        {
            this.importCommand = new RelayCommand(() =>
            {
                try
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    Nullable<bool> result = dlg.ShowDialog();

                    if (result == true)
                    {
                        var itemsToImport = TestItemsImporter.ReadTestItems(dlg.FileName);
                        foreach (var item in itemsToImport)
                        {
                            this.AddItem(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateSaveCommand()
        {
            this.save = new RelayCommand(() =>
            {
                try
                {
                    this.selectedTest.Items.Clear();

                    foreach (TestItem item in this.items)
                    {
                        this.selectedTest.Items.Add(item);
                    }

                    if (this.selectedTest.Id > 0)
                    {
                        this.testSetsModel.Update(this.selectedTest);
                    }
                    else
                    {
                        this.testSetsModel.Add(this.selectedTest);
                    }

                    ApplicationMessage.Send(MessageType.TestsDisplay, MessageType.TestsDisplay.ToString());
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateQuestionDeleteCommand()
        {
            this.questionDelete = new RelayCommand<TestItem>(item =>
            {
                try
                {
                    DeleteConfirmWindow win = new DeleteConfirmWindow();
                    if (win.ShowDialog() ?? false)
                    {
                        this.items.Remove(item);
                        this.selectedItem = null;
                        base.RaisePropertyChanged("Questions");
                        base.RaisePropertyChanged("SelectedQuestion");
                        base.RaisePropertyChanged("QuestionQuantity");
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateQuestionAddCommand()
        {
            this.questionAdd = new RelayCommand(() =>
            {
                try
                {
                    TestItemEditView win = new TestItemEditView();
                    if (win.ShowDialog() ?? false)
                    {
                        TestItem item = new TestItem()
                        {
                            Question = win.Question,
                            Answer = win.Answer,
                            Example = win.Example
                        };

                        this.AddItem(item);
                    }
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void AddItem(TestItem item)
        {
            this.items.Add(item);
            base.RaisePropertyChanged("Questions");
            base.RaisePropertyChanged("QuestionQuantity");
        }

        private void CreateQuestionEditCommand()
        {
            this.questionEdit = new RelayCommand<TestItem>(item =>
            {
                try
                {
                    TestItemEditView win = new TestItemEditView(item);
                    if (win.ShowDialog() ?? false)
                    {
                        this.selectedItem = item;
                        this.items.Remove(this.selectedItem);
                        item.Question = win.Question;
                        item.Answer = win.Answer;
                        item.Example = win.Example;
                        this.items.Add(item);
                    }

                    this.SelectedQuestion = null;

                    base.RaisePropertyChanged("SelectedQuestion");
                    base.RaisePropertyChanged("Questions");
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
