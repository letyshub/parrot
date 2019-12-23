using GalaSoft.MvvmLight.Messaging;
using Letys.Parrot.Messages.Enum;
using System;

namespace Letys.Parrot.Messages
{
    public static class ApplicationMessage
    {
        public static void Send<T>(MessageType messageType, T message)
        {
            Messenger.Default.Send(message, messageType);
        }

        public static void Register<T>(object recipient, MessageType messageType, Action<T> action)
        {
            Messenger.Default.Register(recipient, messageType, action);
        }
    }
}
