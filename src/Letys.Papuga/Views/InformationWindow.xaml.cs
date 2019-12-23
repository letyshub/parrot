using System.Windows;

namespace Letys.Parrot.Views
{
    public partial class InformationWindow : Window
    {
        public InformationWindow(string message)
        {
            this.Message = message;
            this.DataContext = this;
            InitializeComponent();
        }

        public string Message { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
