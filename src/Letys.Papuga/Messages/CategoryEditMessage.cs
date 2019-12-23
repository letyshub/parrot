using GalaSoft.MvvmLight.Messaging;
using Letys.Papuga.Messages.Enum;
using System;

namespace Letys.Papuga.Messages
{
    public class CategoryEditMessage
    {
        public static void Send()
        {
            Messenger.Default.Send("Category edit", MessageType.CategoryEdit);
        }

        public static void Register(object recipient, Action<string> action)
        {
            Messenger.Default.Register(recipient, MessageType.CategoryEdit, action);
        }
    }
}
