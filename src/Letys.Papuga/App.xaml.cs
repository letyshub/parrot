using GalaSoft.MvvmLight.Threading;
using Letys.Parrot.Bootstrap;
using System;
using System.Windows;

namespace Letys.Parrot
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                BootStrapper.Start();
                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }
    }
}
