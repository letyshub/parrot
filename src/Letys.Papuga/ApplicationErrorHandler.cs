using Letys.Parrot.Log;
using Letys.Parrot.Messages;
using Letys.Parrot.Messages.Enum;
using Letys.Parrot.Views;
using System;

namespace Letys.Parrot
{
    public static class ApplicationErrorHandler
    {
        public static void HandleException(Exception ex)
        {
            Logger.Error(ex);
            ErrorWindow win = new ErrorWindow();
            win.ShowDialog();
            ApplicationMessage.Send(MessageType.CloseApp, MessageType.CloseApp.ToString());
        }
    }
}
