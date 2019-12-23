using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Core;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Model;
using System;

namespace Letys.Parrot.ViewModel
{
    public class CategoryViewModel : ApplicationViewModel
    {
        private ICategoriesModel model;

        public CategoryViewModel(Category category, ICategoriesModel model)
        {
            this.model = model;
            this.Category = category;
        }

        public Category Category { get; }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                if (this.cancelCommand == null)
                {
                    this.CreateCancelCommand();
                }

                return this.cancelCommand;
            }
        }

        public RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.CreateSaveCommand();
                }

                return this.saveCommand;
            }
        }

        private void CreateSaveCommand()
        {
            this.saveCommand = new RelayCommand(() =>
            {
                try
                {
                    if (this.Category.Id > 0)
                    {
                        this.model.Update(this.Category);
                    }
                    else
                    {
                        this.model.Add(this.Category);
                    }

                    ApplicationMessage.Send(MessageType.CategoriesDisplay, MessageType.CategoriesDisplay.ToString());
                }
                catch (Exception ex)
                {
                    ApplicationErrorHandler.HandleException(ex);
                }
            });
        }

        private void CreateCancelCommand()
        {
            try
            {
                if (this.cancelCommand == null)
                {
                    this.cancelCommand = new RelayCommand(() =>
                    {
                        ApplicationMessage.Send(MessageType.CategoriesDisplay, MessageType.CategoriesDisplay.ToString());
                    });
                }
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }
    }
}
