using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using System;

namespace Letys.Parrot.ViewModel
{
    public abstract class ApplicationViewModel : ViewModelBase
    {
        #region Command

        private RelayCommand previousCommand;
        public RelayCommand PreviousCommand
        {
            get
            {
                if (this.previousCommand == null)
                {
                    this.CreatePreviousCommand();
                }

                return this.previousCommand;
            }
        }

        #endregion

        private void CreatePreviousCommand()
        {
            try
            {
                this.previousCommand = new RelayCommand(() =>
                {
                    ApplicationMessage.Send(MessageType.PreviousViewDisplay, "Display previous view");
                });
            }
            catch (Exception ex)
            {
                ApplicationErrorHandler.HandleException(ex);
            }
        }
    }
}
