using Letys.Parrot.Model;
using Letys.Parrot.ViewModel;
using System.Windows;

namespace Letys.Parrot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainViewModel();
            InitializeComponent();
        }
    }
}
