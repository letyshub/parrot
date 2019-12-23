using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using Letys.Parrot.Views;
using System;
using System.Collections.ObjectModel;

namespace Letys.Parrot.ViewModel
{
    public class CategoriesViewModel : ViewModelBase
    {
        private ICategoriesModel model;
        private ObservableCollection<Category> categories;

        public CategoriesViewModel(ICategoriesModel model)
        {
            try
            {
                this.model = model;
                this.GetAllCategories();
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        #region Commands

        private RelayCommand addCategory;
        public RelayCommand AddCategory
        {
            get
            {
                if (this.addCategory == null)
                {
                    this.CreateAddCategoryCommand();
                }

                return this.addCategory;
            }
        }

        private RelayCommand<Category> editCategory;
        public RelayCommand<Category> EditCategory
        {
            get
            {
                if (this.editCategory == null)
                {
                    this.CreateEditCategoryCommand();
                }

                return this.editCategory;
            }
        }

        private RelayCommand<Category> deleteCategory;
        public RelayCommand<Category> DeleteCategory
        {
            get
            {
                if (this.deleteCategory == null)
                {
                    this.CreateDeleteCategoryCommand();
                }

                return this.deleteCategory;
            }
        }

        #endregion

        #region Properties

        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }
        }

        #endregion

        #region Private methods

        private void GetAllCategories()
        {
            this.categories = this.model.GetAll();
            base.RaisePropertyChanged("Categories");
        }

        private void CreateAddCategoryCommand()
        {
            try
            {
                this.addCategory = new RelayCommand(() =>
                {
                    ApplicationMessage.Send(MessageType.CategoryAdd, MessageType.CategoryAdd.ToString());
                });
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }

        private void CreateEditCategoryCommand()
        {
            this.editCategory = new RelayCommand<Category>(category =>
            {
                try
                {
                    ApplicationMessage.Send(MessageType.CategoryEdit, category);
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateDeleteCategoryCommand()
        {
            this.deleteCategory = new RelayCommand<Category>(category =>
            {
                try
                {
                    DeleteConfirmWindow win = new DeleteConfirmWindow();
                    if (win.ShowDialog() == true)
                    {
                        this.model.Delete(category);
                        this.GetAllCategories();
                    }
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
